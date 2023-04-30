using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.PitStops.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.PitStops.Ports
{
    public interface IPitStopsRepository : IJsonDataRepository
    {
        Task<IEnumerable<PitStop>> GetPitStopsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<PitStop>> GetPitStopsInRangeAsync(
            SeriesTypes seriesId,
            int raceId,
            int? startLap,
            int? endLap = null,
            int? carNumber = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
