using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LiveFeeds.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.LiveFeeds.Ports
{
    public interface IKeyMomentsRepository : IJsonDataRepository
    {
        Task<IEnumerable<KeyMoment>> GetKeyMomentsAsync(
            SeriesTypes seriesId, 
            int raceId, 
            int? year = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
