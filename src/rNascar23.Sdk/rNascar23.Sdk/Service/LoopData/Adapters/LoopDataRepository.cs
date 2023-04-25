using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LoopData.Models;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Service.LoopData.Data.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LoopData.Adapters
{
    internal class LoopDataRepository : JsonDataRepository, ILoopDataRepository
    {
        #region fields

        protected readonly IMapper _mapper;

        #endregion

        #region properties

        // https://cf.nascar.com/loopstats/prod/2023/2/5314.json
        protected virtual string Url { get => @"https://cf.nascar.com/loopstats/prod/{0}/{1}/{2}.json"; }

        #endregion

        #region fields

        public LoopDataRepository(IMapper mapper, ILogger<LoopDataRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<EventLoopData> GetLoopDataAsync(
            SeriesTypes seriesId, 
            int raceId, 
            int? year = null, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var absoluteUrl = BuildUrl(seriesId, raceId, year);

                var json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = JsonConvert.DeserializeObject<EventLoopDataModel[]>(json);

                    if (model != null)
                    {
                        var raceStats = model.FirstOrDefault();

                        return _mapper.Map<EventLoopData>(raceStats);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading loop data: seriesId:{seriesId}; raceId:{raceId}");
            }

            return new EventLoopData();
        }

        #endregion

        #region protected

        protected virtual string BuildUrl(SeriesTypes seriesId, int raceId, int? year = null)
        {
            return String.Format(Url, year.GetValueOrDefault(DateTime.Now.Year), (int)seriesId, raceId);
        }

        #endregion
    }
}
