using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LiveFeeds.Adapters
{
    internal class KeyMomentsRepository : ResettableCircuitBreakerRepository, IKeyMomentsRepository
    {
        #region fields

        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public KeyMomentsRepository(
            ILogger<KeyMomentsRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        #endregion

        #region public

        public virtual async Task<IEnumerable<KeyMoment>> GetKeyMomentsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                CheckForNewRaceId(raceId);

                if (!CircuitBreakerTripped)
                {
                    var url = _apiSourcesRepository.GetApiUrl(
                   ApiSourceType.KeyMoments,
                   (int)seriesId,
                   raceId,
                   year);

                    var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

                    if (!string.IsNullOrEmpty(json))
                    {
                        var model = JsonConvert.DeserializeObject<KeyMomentModelList>(json);

                        if (model != null)
                        {
                            var keyMomentsList = new List<KeyMoment>();

                            foreach (KeyValuePair<int, KeyMomentModel[]> lapModel in model.Laps)
                            {
                                for (int i = 0; i < lapModel.Value.Length; i++)
                                {
                                    keyMomentsList.Add(new KeyMoment()
                                    {
                                        LapNumber = lapModel.Key,
                                        Note = lapModel.Value[i].Note,
                                        NoteId = lapModel.Value[i].NoteID,
                                        FlagState = lapModel.Value[i].FlagState,
                                    });
                                }
                            }

                            var enumerable = keyMomentsList as IEnumerable<KeyMoment>;

                            if (skip.HasValue)
                                enumerable = enumerable.Skip(skip.Value);

                            if (take.HasValue)
                                enumerable = enumerable.Take(take.Value);

                            return enumerable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception reading KeyMoments");
            }

            return new List<KeyMoment>();
        }

        #endregion
    }
}
