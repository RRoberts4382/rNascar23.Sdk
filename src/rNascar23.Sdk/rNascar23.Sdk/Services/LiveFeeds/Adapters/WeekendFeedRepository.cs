using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LiveFeeds.Adapters
{
    internal class WeekendFeedRepository : ResettableCircuitBreakerRepository, IWeekendFeedRepository
    {
        #region fields

        private readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public WeekendFeedRepository(
            IMapper mapper,
            ILogger<WeekendFeedRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        #endregion

        #region public

        public virtual async Task<WeekendFeed> GetWeekendFeedAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                CheckForNewRaceId(raceId);

                if (!CircuitBreakerTripped)
                {
                    var url = _apiSourcesRepository.GetApiUrl(
                   ApiSourceType.WeekendFeed,
                   (int)seriesId,
                   raceId,
                   year);

                    var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(json))
                    {
                        var model = JsonConvert.DeserializeObject<WeekendFeedModel>(json);

                        return _mapper.Map<WeekendFeed>(model);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading weekend feed data. seriesId:{seriesId}, raceId:{raceId}, year:{year}");
            }

            return new WeekendFeed();
        }

        #endregion
    }
}
