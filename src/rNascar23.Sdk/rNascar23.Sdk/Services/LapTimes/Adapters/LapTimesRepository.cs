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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LapTimes.Adapters
{
    internal class LapTimesRepository : JsonDataRepository, ILapTimesRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public LapTimesRepository(
            IMapper mapper,
            ILogger<LapTimesRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
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
                var url = _apiSourcesRepository.GetApiUrl(
                    ApiSourceType.LapTimes,
                    (int)seriesId,
                    raceId,
                    year);

                json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

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
                var url = _apiSourcesRepository.GetApiUrl(
                    ApiSourceType.LapTimes,
                    (int)seriesId,
                    raceId,
                    year);

                json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

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
    }
}
