using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.PitStops.Models;
using rNascar23.Sdk.PitStops.Ports;
using rNascar23.Sdk.Service.PitStops.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.PitStops.Adapters
{
    internal class PitStopsRepository : JsonDataRepository, IPitStopsRepository
    {
        #region fields

        protected readonly IMapper _mapper;

        #endregion

        #region properties

        // https://cf.nascar.com/cacher/live/series_2/5314/live-pit-data.json
        protected virtual string Url { get => @"https://cf.nascar.com/cacher/live/series_{0}/{1}/live-pit-data.json"; }

        #endregion

        #region ctor

        public PitStopsRepository(IMapper mapper, ILogger<PitStopsRepository> logger)
          : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<IEnumerable<PitStop>> GetPitStopsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            IList<PitStop> pitStops = new List<PitStop>();

            string json = string.Empty;

            try
            {
                var absoluteUrl = BuildUrl(seriesId, raceId);

                json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var models = JsonConvert.DeserializeObject<IList<PitStopModel>>(json);

                    if (models != null)
                    {
                        pitStops = _mapper.Map<IList<PitStop>>(models);

                        var enumerable = pitStops as IEnumerable<PitStop>;

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
                ExceptionHandler(ex, $"Error in PitStopRepository. SeriesId: {seriesId}, RaceId: {raceId}", json);
            }

            return pitStops;
        }

        public virtual async Task<IEnumerable<PitStop>> GetPitStopsInRangeAsync(
            SeriesTypes seriesId,
            int raceId,
            int? startLap,
            int? endLap = null,
            int? carNumber = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            IList<PitStop> pitStops = new List<PitStop>();

            string json = string.Empty;

            try
            {
                var absoluteUrl = BuildUrl(seriesId, raceId);

                json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var models = JsonConvert.DeserializeObject<IList<PitStopModel>>(json);

                    if (models != null)
                    {
                        IEnumerable<PitStopModel> filteredModels = models;

                        if (carNumber.HasValue)
                            filteredModels = filteredModels.Where(p => p.vehicle_number == carNumber.Value.ToString());

                        if (startLap.HasValue)
                            filteredModels = filteredModels.Where(p => p.lap_count >= startLap.Value);

                        if (endLap.HasValue)
                            filteredModels = filteredModels.Where(p => p.lap_count <= endLap.Value);

                        if (skip.HasValue)
                            filteredModels = filteredModels.Skip(skip.Value);

                        if (take.HasValue)
                            filteredModels = filteredModels.Take(take.Value);

                        return _mapper.Map<IList<PitStop>>(filteredModels.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error in PitStopRepository. SeriesId: {seriesId}, RaceId: {raceId}", json);
            }

            return new List<PitStop>();
        }

        #endregion

        #region private

        protected virtual string BuildUrl(SeriesTypes seriesId, int raceId)
        {
            return String.Format(Url, (int)seriesId, raceId);
        }

        protected virtual void ExceptionHandler(Exception ex, string message, string json)
        {
            _logger.LogError(ex, $"{message}\r\n\r\njson: {json}");
        }

        #endregion
    }
}
