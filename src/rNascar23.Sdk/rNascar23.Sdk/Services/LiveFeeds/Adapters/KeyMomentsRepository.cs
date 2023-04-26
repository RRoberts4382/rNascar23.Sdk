using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.LiveFeeds.Adapters
{
    internal class KeyMomentsRepository : JsonDataRepository, IKeyMomentsRepository
    {
        #region properties

        // https://cf.nascar.com/cacher/2023/2/5314/lap-notes.json
        protected virtual string Url { get => @"https://cf.nascar.com/cacher/{0}/{1}/{2}/lap-notes.json"; }

        #endregion

        #region ctor

        public KeyMomentsRepository(ILogger<KeyMomentsRepository> logger)
            : base(logger)
        {
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
                var absoluteUrl = BuildUrl(seriesId, raceId, year);

                var json = await GetAsync(absoluteUrl, cancellationToken).ConfigureAwait(false);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception reading KeyMoments");
            }

            return new List<KeyMoment>();
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
