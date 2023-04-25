using AutoMapper;
using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Data.LiveFeeds.Ports;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LiveFeeds.Adapters
{
    public class LiveFeedRepository : JsonDataRepository, ILiveFeedRepository
    {
        #region fields

        protected readonly IMapper _mapper;

        #endregion

        #region properties

        protected virtual string Url { get => @"https://cf.nascar.com/live/feeds/live-feed.json"; }

        #endregion

        #region ctor

        public LiveFeedRepository(IMapper mapper, ILogger<LiveFeedRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<LiveFeed> GetLiveFeedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var json = await GetAsync(Url, cancellationToken).ConfigureAwait(false);

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
