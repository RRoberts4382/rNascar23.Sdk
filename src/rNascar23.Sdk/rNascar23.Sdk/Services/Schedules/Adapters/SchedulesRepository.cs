using AutoMapper;
using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Schedules.Models;
using rNascar23.Sdk.Schedules.Ports;
using rNascar23.Sdk.Service.Schedules.Data.Models;
using rNascar23.Sdk.Services.Schedules.Logic;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.Schedules.Adapters
{
    internal class SchedulesRepository : JsonDataRepository, ISchedulesRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;
        protected ScheduleCache _cache;
        protected readonly TimeSpan _cacheDuration = new TimeSpan(0, 15, 0);

        #endregion

        #region ctor

        public SchedulesRepository(
            IMapper mapper,
            ILogger<SchedulesRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        #endregion

        #region public

        public virtual async Task<SeriesSchedules> GetSchedulesAsync(
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

                var url = _apiSourcesRepository.GetApiUrl(
                    ApiSourceType.Schedules,
                    year);

                var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

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

        public async Task<IList<SeriesEvent>> GetSchedulesAsync(
            ScheduleType scheduleType,
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (scheduleType == ScheduleType.Historical)
                    return new List<SeriesEvent>();

                var schedules = await GetSchedulesAsync(year);

                if (schedules != null)
                {
                    switch (scheduleType)
                    {
                        case ScheduleType.Trucks:
                            {
                                return schedules.TruckSeries;
                            }
                        case ScheduleType.Xfinity:
                            {
                                return schedules.XfinitySeries;
                            }
                        case ScheduleType.Cup:
                            {
                                return schedules.CupSeries;
                            }
                        case ScheduleType.All:
                            {
                                return schedules.CupSeries.Concat(schedules.XfinitySeries).Concat(schedules.TruckSeries).ToList();
                            }
                        case ScheduleType.ThisWeek:
                            {
                                var range = DayOfWeekHelper.GetScheduleRange(scheduleType);

                                return schedules.CupSeries.
                                    Concat(schedules.XfinitySeries).
                                    Concat(schedules.TruckSeries).
                                    Where(s => s.DateScheduled.Date >= range.Start.Date && s.DateScheduled.Date <= range.End.Date).
                                    ToList();
                            }
                        case ScheduleType.NextWeek:
                            {
                                var range = DayOfWeekHelper.GetScheduleRange(scheduleType);

                                return schedules.CupSeries.
                                    Concat(schedules.XfinitySeries).
                                    Concat(schedules.TruckSeries).
                                    Where(s => s.DateScheduled.Date >= range.Start.Date && s.DateScheduled.Date <= range.End.Date).
                                    ToList();
                            }
                        case ScheduleType.Today:
                            {
                                return schedules.CupSeries.
                                   Concat(schedules.XfinitySeries).
                                   Concat(schedules.TruckSeries).
                                   Where(s => s.EventActivities.Any(x => x.StartTimeLocal.Date == DateTime.Now.Date)).
                                   ToList();
                            }
                        default:
                            {
                                throw new ArgumentException($"Error selecting schedule to read: Unrecognized Series {scheduleType}", nameof(scheduleType));
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, "Exception reading schedules");
            }

            return new List<SeriesEvent>();
        }

        public async Task<SeriesEvent> GetEventAsync(
            int raceId,
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var schedules = await GetSchedulesAsync(year);

                if (schedules != null)
                {
                    return schedules.AllSeries.FirstOrDefault(s => s.RaceId == raceId);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, "Exception reading schedules");
            }

            return null;
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
