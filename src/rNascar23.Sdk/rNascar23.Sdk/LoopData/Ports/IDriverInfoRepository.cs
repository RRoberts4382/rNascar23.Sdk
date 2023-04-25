using rNascar23.Sdk.Common;
using rNascar23.Sdk.LoopData.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.LoopData.Ports
{
    public interface IDriverInfoRepository
    {
        Task<DriverInfo> GetDriverAsync(
            int id, CancellationToken
            cancellationToken = default);

        Task<IEnumerable<DriverInfo>> GetDriversAsync(
            bool updateFromCompletedRaces = false,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<DriverInfo>> GetDriversAsync(
            SeriesTypes seriesId,
            int raceId,
            int year,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
