using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Points.Models;
using rNascar23.Sdk.Points.Ports;
using rNascar23.Sdk.Service.Points.Data.Models;
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

        #endregion

        #region properties

        protected virtual string Url { get => @"https://cf.nascar.com/live/feeds/series_{0}/{1}/live_points.json"; }

        #endregion

        #region ctor

        public PointsRepository(IMapper mapper, ILogger<PointsRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                var absoluteUrl = BuildUrl(seriesId, raceId);

                var json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = JsonConvert.DeserializeObject<DriverPointsModel[]>(json);

                    if (model != null)
                    {
                        var points =  _mapper.Map<IList<DriverPoints>>(model);

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
                var absoluteUrl = BuildUrl(seriesId, raceId);

                var json = await GetAsync(absoluteUrl).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var models = JsonConvert.DeserializeObject<StagePointsDetailsModel[]>(json);

                    if (models != null)
                    {
                        var points =  _mapper.Map<IList<StagePointsDetails>>(models);

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

        #region protected

        protected virtual string BuildUrl(SeriesTypes seriesId, int raceId)
        {
            return String.Format(Url, (int)seriesId, raceId);
        }

        #endregion
    }
}
