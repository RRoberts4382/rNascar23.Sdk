using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LapTimes.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.LapTimes.Ports
{
    public interface ILapAveragesRepository : IJsonDataRepository
    {
        Task<IEnumerable<LapAverages>> GetLapAveragesAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
