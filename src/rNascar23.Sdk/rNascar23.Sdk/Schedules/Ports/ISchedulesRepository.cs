using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Schedules.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Schedules.Ports
{
    public interface ISchedulesRepository : IJsonDataRepository
    {
        Task<SeriesSchedules> GetRaceListAsync(
            int? year = null, 
            CancellationToken cancellationToken=default);
    }
}
