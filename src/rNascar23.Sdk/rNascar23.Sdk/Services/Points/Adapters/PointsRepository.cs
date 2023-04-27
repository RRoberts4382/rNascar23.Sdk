using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Points.Models;
using rNascar23.Sdk.Points.Ports;
using rNascar23.Sdk.Service.Points.Data.Models;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.Points.Adapters
{
    internal class PointsRepository : JsonDataRepository, IPointsRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public PointsRepository(
            IMapper mapper,
            ILogger<PointsRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        #endregion

        #region public

        public virtual async Task<IEnumerable<DriverPoints>> GetDriverPointsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var url = _apiSourcesRepository.GetApiUrl(
                    ApiSourceType.LoopData,
                    (int)seriesId,
                    raceId);

                var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = JsonConvert.DeserializeObject<DriverPointsModel[]>(json);

                    if (model != null)
                    {
                        var points = _mapper.Map<IList<DriverPoints>>(model);

                        var enumerable = points as IEnumerable<DriverPoints>;

                        if (skip.HasValue)
                            enumerable = enumerable.Skip(skip.Value);

                        if (take.HasValue)
                            enumerable = enumerable.Take(take.Value);

                        return enumerable;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading driver points. seriesId:{seriesId}; raceId:{raceId}");
            }

            return new List<DriverPoints>();
        }

        public virtual async Task<IEnumerable<StagePointsDetails>> GetStagePointsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var url = _apiSourcesRepository.GetApiUrl(
                    ApiSourceType.LoopData,
                    (int)seriesId,
                    raceId);

                var json = await GetAsync(url).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var models = JsonConvert.DeserializeObject<StagePointsDetailsModel[]>(json);

                    if (models != null)
                    {
                        var points = _mapper.Map<IList<StagePointsDetails>>(models);

                        var enumerable = points as IEnumerable<StagePointsDetails>;

                        if (skip.HasValue)
                            enumerable = enumerable.Skip(skip.Value);

                        if (take.HasValue)
                            enumerable = enumerable.Take(take.Value);

                        return enumerable;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading driver stage points. seriesId:{seriesId}; raceId:{raceId}");
            }

            return new List<StagePointsDetails>();
        }

        #endregion
    }
}
