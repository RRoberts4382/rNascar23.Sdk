using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LiveFeeds.Adapters
{
    internal class WeekendFeedRepository : JsonDataRepository, IWeekendFeedRepository
    {
        #region fields

        private readonly IMapper _mapper;

        #endregion

        #region properties

        // https://cf.nascar.com/cacher/2023/1/5274/weekend-feed.json
        protected virtual string Url { get => @"https://cf.nascar.com/cacher/{0}/{1}/{2}/weekend-feed.json"; }

        #endregion

        #region ctor

        public WeekendFeedRepository(IMapper mapper, ILogger<WeekendFeedRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                var absoluteUrl = BuildUrl(seriesId, raceId, year);

                var json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = JsonConvert.DeserializeObject<WeekendFeedModel>(json);

                    return _mapper.Map<WeekendFeed>(model);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading weekend feed data. seriesId:{seriesId}, raceId:{raceId}, year:{year}");
            }

            return new WeekendFeed();
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
