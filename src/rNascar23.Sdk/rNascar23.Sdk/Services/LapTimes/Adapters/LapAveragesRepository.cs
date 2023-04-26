using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23.Sdk.Service.LapTimes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LapTimes.Adapters
{
    internal class LapAveragesRepository : JsonDataRepository, ILapAveragesRepository
    {
        #region consts

        protected const int CircuitBreakerLimit = 2;

        #endregion

        #region fields

        protected readonly IMapper _mapper;
        private int _errorCount = 0;

        #endregion

        #region properties

        protected virtual bool CircuitBreakerTripped
        {
            get
            {
                return _errorCount >= CircuitBreakerLimit;
            }
        }

        // https://cf.nascar.com/cacher/2023/2/5314/lap-times.json
        protected virtual string Url { get => @"https://cf.nascar.com/cacher/{0}/{1}/{2}/lap-averages.json"; }

        #endregion

        #region ctor

        public LapAveragesRepository(IMapper mapper, ILogger<LapAveragesRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<IEnumerable<LapAverages>> GetLapAveragesAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            string json = String.Empty;

            try
            {
                if (!CircuitBreakerTripped)
                {
                    var absoluteUrl = BuildUrl(seriesId, raceId, year);

                    json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                    if (String.IsNullOrEmpty(json))
                    {
                        IncrementErrorCount();
                    }
                    else
                    {
                        var model = JsonConvert.DeserializeObject<LapAveragesDataModel[]>(json);

                        if (model != null)
                        {
                            var averages =  _mapper.Map<List<LapAverages>>(model[0].Items);

                            var enumerable = averages as IEnumerable<LapAverages>;

                            if (skip.HasValue)
                                enumerable = enumerable.Skip(skip.Value);

                            if (take.HasValue)
                                enumerable = enumerable.Take(take.Value);

                            return enumerable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler
                (
                    ex,
                    $"Error reading lap average data: {ex.Message}",
                    json
                );
            }

            return new List<LapAverages>();
        }

        #endregion

        #region protected
        
        protected virtual string BuildUrl(SeriesTypes seriesId, int raceId, int? year = null)
        {
            return String.Format(Url, year.GetValueOrDefault(DateTime.Now.Year), (int)seriesId, raceId);
        }

        protected virtual void ExceptionHandler(Exception ex, string message, string json)
        {
            _logger.LogError(ex, $"{message}\r\n\r\njson: {json}\r\nError Count: {_errorCount}");

            IncrementErrorCount();
        }

        #endregion

        #region private

        private void IncrementErrorCount()
        {
            _errorCount += 1;

            if (CircuitBreakerTripped)
                _logger.LogError("*** Circuit Breaker Tripped in LapAveragesRepository ***");
        }

        #endregion
    }
}
