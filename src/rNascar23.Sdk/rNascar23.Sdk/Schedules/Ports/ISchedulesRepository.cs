using rNascar23.Sdk.Schedules.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Schedules.Ports
{
    public interface ISchedulesRepository
    {
        Task<SeriesSchedules> GetRaceListAsync(
            int? year = null, 
            CancellationToken cancellationToken=default);
    }
}
