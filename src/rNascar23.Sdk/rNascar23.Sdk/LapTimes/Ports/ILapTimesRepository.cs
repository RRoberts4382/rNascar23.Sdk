using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LapTimes.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.LapTimes.Ports
{
    public interface ILapTimesRepository : IJsonDataRepository
    {
        Task<LapTimeData> GetLapTimeDataAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            CancellationToken cancellationToken = default);

        Task<LapTimeData> GetLapTimeDataAsync(
            SeriesTypes seriesId,
            int raceId,
            int driverId,
            int? year = null,
            CancellationToken cancellationToken = default);
    }
}
