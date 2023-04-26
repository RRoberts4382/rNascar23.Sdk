using AutoMapper;
using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Schedules.Models;
using rNascar23.Sdk.Schedules.Ports;
using rNascar23.Sdk.Service.Schedules.Data.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.Schedules.Adapters
{
    internal class SchedulesRepository : JsonDataRepository, ISchedulesRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected ScheduleCache _cache;
        protected readonly TimeSpan _cacheDuration = new TimeSpan(0, 15, 0);

        #endregion

        #region properties

        protected virtual string Url { get => @"https://cf.nascar.com/cacher/{0}/race_list_basic.json"; }

        #endregion

        #region ctor

        public SchedulesRepository(IMapper mapper, ILogger<SchedulesRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<SeriesSchedules> GetRaceListAsync(
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (_cache != null)
                {
                    if (DateTime.Now.Subtract(_cache.Timestamp) < _cacheDuration)
                        return _cache.SeriesSchedules;
                }

                var absoluteUrl = BuildUrl(year);

                var json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = Newtonsoft.Json.JsonConvert.DeserializeObject<SeriesEventModel>(json);

                    if (model != null)
                    {
                        var seriesSchedules = _mapper.Map<SeriesSchedules>(model);

                        foreach (var seriesEvent in seriesSchedules.AllSeries)
                        {
                            foreach (var seriesEventActivity in seriesEvent.EventActivities)
                            {
                                seriesEventActivity.SeriesId = seriesEvent.SeriesId;
                            }
                        }

                        _cache = new ScheduleCache()
                        {
                            Timestamp = DateTime.Now,
                            SeriesSchedules = seriesSchedules
                        };

                        return seriesSchedules;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, "Exception reading schedules");
            }

            return new SeriesSchedules();
        }

        #endregion

        #region protected

        protected virtual string BuildUrl(int? year = null)
        {
            return String.Format(Url, year.GetValueOrDefault(DateTime.Now.Year));
        }

        #endregion

        #region classes

        protected class ScheduleCache
        {
            public DateTime Timestamp { get; set; }
            public SeriesSchedules SeriesSchedules { get; set; }
        }

        #endregion
    }
}
