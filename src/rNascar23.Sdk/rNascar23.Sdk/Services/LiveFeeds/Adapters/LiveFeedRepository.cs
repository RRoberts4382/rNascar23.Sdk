using AutoMapper;
using Microsoft.Extensions.Logging;
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
    public class LiveFeedRepository : JsonDataRepository, ILiveFeedRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public LiveFeedRepository(
            IMapper mapper,
            ILogger<LiveFeedRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        #endregion

        #region public

        public virtual async Task<LiveFeed> GetLiveFeedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var url = _apiSourcesRepository.GetApiUrl(ApiSourceType.LiveFeed);

                var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = Newtonsoft.Json.JsonConvert.DeserializeObject<LiveFeedModel>(json);

                    if (model != null)
                    {
                        return _mapper.Map<LiveFeed>(model);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, "Error loading live feed");
            }

            return null;
        }

        #endregion
    }
}
