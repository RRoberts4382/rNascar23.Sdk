using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Points.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Points.Ports
{
    public interface IPointsRepository : IJsonDataRepository
    {
        Task<IEnumerable<DriverPoints>> GetDriverPointsAsync(
            SeriesTypes seriesId, 
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<StagePointsDetails>> GetStagePointsAsync(
            SeriesTypes seriesId, 
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
