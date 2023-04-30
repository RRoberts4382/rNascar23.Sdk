using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23.Sdk.Service.LapTimes.Data.Models;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LapTimes.Adapters
{
    internal class LapAveragesRepository : ResettableCircuitBreakerRepository, ILapAveragesRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public LapAveragesRepository(
            IMapper mapper, 
            ILogger<LapAveragesRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
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
                CheckForNewRaceId(raceId);

                if (!CircuitBreakerTripped)
                {
                    var url = _apiSourcesRepository.GetApiUrl(
                        ApiSourceType.LapAverages, 
                        (int)seriesId, 
                        raceId, 
                        year);

                    json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

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
    }
}
