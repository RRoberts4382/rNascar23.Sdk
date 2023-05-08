using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LoopData.Models;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Service.LoopData.Data.Models;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LoopData.Adapters
{
    internal class LoopDataRepository : ResettableCircuitBreakerRepository, ILoopDataRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;
        protected readonly IDriverInfoRepository _driverInfoRepository;

        #endregion

        #region ctor

        public LoopDataRepository(
            IMapper mapper,
            ILogger<LoopDataRepository> logger,
            IDriverInfoRepository driverInfoRepository,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        #endregion

        #region public

        public virtual async Task<EventLoopData> GetLoopDataAsync(
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
                    ApiSourceType.LoopData,
                    (int)seriesId,
                    raceId,
                    year);

                    var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(json))
                    {
                        var model = JsonConvert.DeserializeObject<EventLoopDataModel[]>(json);

                        if (model != null)
                        {
                            var raceStats = model.FirstOrDefault();

                            return _mapper.Map<EventLoopData>(raceStats);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading loop data: seriesId:{seriesId}; raceId:{raceId}");
            }

            return new EventLoopData();
        }

        public virtual async Task<IList<DriverRatingInfo>> GetLoopDataRatingsAsync(
            SeriesTypes seriesId,
            int raceId,
            int take = 10,
            int? year = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                CheckForNewRaceId(raceId);

                if (!CircuitBreakerTripped)
                {
                    var url = _apiSourcesRepository.GetApiUrl(
                    ApiSourceType.LoopData,
                    (int)seriesId,
                    raceId,
                    year);

                    var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(json))
                    {
                        var model = JsonConvert.DeserializeObject<EventLoopDataModel[]>(json);

                        if (model != null)
                        {
                            var raceStats = model.FirstOrDefault();

                            var eventLoopData = _mapper.Map<EventLoopData>(raceStats);

                            var driverList = await _driverInfoRepository.GetDriversAsync();

                            return eventLoopData.Drivers.OrderByDescending(e => e.Rating).
                                Take(take).
                                Select((e, i) => new DriverRatingInfo()
                                {
                                    Position = i + 1,
                                    Driver = driverList.FirstOrDefault(d => d.Id == e.DriverId)?.Name,
                                    Rating = e.Rating
                                }).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading loop data: seriesId:{seriesId}; raceId:{raceId}");
            }

            return new List<DriverRatingInfo>();
        }

        #endregion

        #region protected



        #endregion
    }
}
