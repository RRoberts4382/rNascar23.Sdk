using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.LoopData.Models;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Schedules.Ports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LoopData.Adapters
{
    internal class DriverInfoRepository : JsonDataRepository, IDriverInfoRepository
    {
        #region consts

        protected const string DataFileName = "DriverInfo.json";

        #endregion

        #region fields

        protected readonly IMapper _mapper;
        protected readonly IWeekendFeedRepository _weekendFeedRepository = null;
        protected readonly ISchedulesRepository _scheduleRepository = null;

        #endregion

        #region ctor

        public DriverInfoRepository(
            IMapper mapper,
            ILogger<DriverInfoRepository> logger,
            IWeekendFeedRepository weekendFeedRepository,
            ISchedulesRepository scheduleRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weekendFeedRepository = weekendFeedRepository ?? throw new ArgumentNullException(nameof(weekendFeedRepository));
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }

        #endregion

        #region public

        public virtual async Task<DriverInfo> GetDriverAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var driverData = LoadDriverData();

                if (driverData.Count == 0)
                {
                    driverData = await LoadDriversFromScheduleAsync(driverData.ToList(), cancellationToken).ConfigureAwait(false);
                }

                if (driverData.Count > 0)
                {
                    return driverData.FirstOrDefault(d => d.Id == id);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error loading driver info for driverId {id}");
            }

            return new DriverInfo();
        }

        public virtual async Task<IEnumerable<DriverInfo>> GetDriversAsync(
            bool updateFromCompletedRaces = false,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var driverData = LoadDriverData();

                if (driverData.Count == 0 || updateFromCompletedRaces)
                {
                    driverData = await LoadDriversFromScheduleAsync(driverData.ToList(), cancellationToken).ConfigureAwait(false);
                }

                if (driverData.Count > 0)
                {
                    var enumerable = driverData as IEnumerable<DriverInfo>;

                    if (skip.HasValue)
                        enumerable = enumerable.Skip(skip.Value);

                    if (take.HasValue)
                        enumerable = enumerable.Take(take.Value);

                    return enumerable;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, "Error loading driver info for all drivers");
            }

            return new List<DriverInfo>();
        }

        public virtual async Task<IEnumerable<DriverInfo>> GetDriversAsync(
            SeriesTypes seriesId,
            int raceId,
            int year,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var driverData = LoadDriverData();

                var eventDrivers = await GetDriversFromEventAsync(seriesId, raceId, year, cancellationToken).ConfigureAwait(false);

                var drivers = MergeDrivers(driverData.ToList(), eventDrivers);

                var enumerable = drivers as IEnumerable<DriverInfo>;

                if (skip.HasValue)
                    enumerable = enumerable.Skip(skip.Value);

                if (take.HasValue)
                    enumerable = enumerable.Take(take.Value);

                return enumerable;
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error loading driver info for seriesId {seriesId}, raceId {raceId}, year {year}");
            }

            return new List<DriverInfo>();
        }

        #endregion

        #region protected

        protected virtual async Task<IList<DriverInfo>> LoadDriversFromScheduleAsync(List<DriverInfo> data, CancellationToken cancellationToken = default)
        {
            var raceLists = await _scheduleRepository.GetSchedulesAsync(null, cancellationToken).ConfigureAwait(false);

            foreach (var race in raceLists.TruckSeries.Where(t => t.DateScheduled.Date < DateTime.Now.Date))
            {
                var eventDrivers = await GetDriversFromEventAsync(race.SeriesId, race.RaceId, race.RaceSeason, cancellationToken).ConfigureAwait(false);

                MergeDrivers(data, eventDrivers);
            }
            foreach (var race in raceLists.XfinitySeries.Where(t => t.DateScheduled.Date < DateTime.Now.Date))
            {
                var eventDrivers = await GetDriversFromEventAsync(race.SeriesId, race.RaceId, race.RaceSeason, cancellationToken).ConfigureAwait(false);

                MergeDrivers(data, eventDrivers);
            }
            foreach (var race in raceLists.CupSeries.Where(t => t.DateScheduled.Date < DateTime.Now.Date))
            {
                var eventDrivers = await GetDriversFromEventAsync(race.SeriesId, race.RaceId, race.RaceSeason, cancellationToken).ConfigureAwait(false);

                MergeDrivers(data, eventDrivers);
            }

            return data;
        }

        protected virtual IList<DriverInfo> MergeDrivers(List<DriverInfo> data, List<DriverInfo> models)
        {
            var driversToAdd = new List<DriverInfo>();

            foreach (var model in models.ToList())
            {
                if (!data.Any(d => d.Id == model.Id))
                {
                    driversToAdd.Add(model);
                }
            }

            if (driversToAdd.Count > 0)
            {
                data.AddRange(driversToAdd);

                SaveDriverInfo(data);
            }

            return data;
        }

        protected virtual async Task<List<DriverInfo>> GetDriversFromEventAsync(
            SeriesTypes seriesId,
            int raceId,
            int year,
            CancellationToken cancellationToken = default)
        {
            List<DriverInfo> models = new List<DriverInfo>();

            var weekendData = await _weekendFeedRepository.GetWeekendFeedAsync(seriesId, raceId, year, cancellationToken).ConfigureAwait(false);

            var raceResults = weekendData.WeekendRaces.FirstOrDefault();

            if (raceResults == null)
            {
                var runResults = weekendData.WeekendRuns.FirstOrDefault();

                if (runResults == null)
                    return models;
                else
                {
                    foreach (var runResult in runResults.Results)
                    {
                        var model = new DriverInfo()
                        {
                            Id = runResult.DriverId,
                            Name = runResult.DriverName
                        };

                        models.Add(model);
                    }
                }
            }
            else
            {
                foreach (var runResult in raceResults.Results)
                {
                    var model = new DriverInfo()
                    {
                        Id = runResult.DriverId,
                        Name = runResult.DriverFullName
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        protected virtual IList<DriverInfo> LoadDriverData()
        {
            var fileName = BuildDataFilePath();

            if (!File.Exists(fileName))
            {
                return new List<DriverInfo>();
            }

            var json = File.ReadAllText(fileName);

            if (string.IsNullOrEmpty(json))
            {
                return new List<DriverInfo>();
            }

            var data = JsonConvert.DeserializeObject<List<DriverInfo>>(json);

            return data;
        }

        protected virtual void SaveDriverInfo(IList<DriverInfo> models)
        {
            var fileName = BuildDataFilePath();

            var json = JsonConvert.SerializeObject(models, Formatting.Indented);

            File.WriteAllText(fileName, json);
        }

        private string BuildDataFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"rNascar23\\Data\\{DataFileName}");
        }

        #endregion
    }
}
