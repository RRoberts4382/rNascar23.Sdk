using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23.Sdk.Service.LapTimes.Data.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LapTimes.Adapters
{
    internal class LapTimesRepository : JsonDataRepository, ILapTimesRepository
    {
        #region fields

        protected readonly IMapper _mapper;

        #endregion

        #region properties

        // https://cf.nascar.com/cacher/2023/2/5314/lap-times.json
        protected virtual string Url { get => @"https://cf.nascar.com/cacher/{0}/{1}/{2}/lap-times.json"; }

        #endregion

        #region ctor

        public LapTimesRepository(IMapper mapper, ILogger<LapTimesRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<LapTimeData> GetLapTimeDataAsync(
            SeriesTypes seriesId, 
            int raceId,
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            string json = String.Empty;

            try
            {
                var absoluteUrl = BuildUrl(seriesId, raceId, year);

                json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = JsonConvert.DeserializeObject<LapTimeDataModel>(json, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    if (model != null)
                    {
                        return _mapper.Map<LapTimeData>(model);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading lap time data: {ex.Message}\r\n\r\nseriesId:{seriesId}; raceId{raceId}; json:{json}\r\n");
            }

            return new LapTimeData();
        }

        public virtual async Task<LapTimeData> GetLapTimeDataAsync(
            SeriesTypes seriesId,
            int raceId,
            int driverId,
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            string json = String.Empty;

            try
            {
                var absoluteUrl = BuildUrl(seriesId, raceId, year);

                json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = JsonConvert.DeserializeObject<LapTimeDataModel>(json);

                    if (model != null)
                    {
                        var lapTimeData = _mapper.Map<LapTimeData>(model);

                        return new LapTimeData()
                        {
                            LapFlags = lapTimeData.LapFlags,
                            Drivers = lapTimeData.Drivers.Where(d => d.NASCARDriverID == driverId).ToList(),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading lap time data: {ex.Message}\r\n\r\nseriesId:{seriesId}; raceId{raceId}; driverId{driverId}; json:{json}\r\n");
            }

            return new LapTimeData();
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
