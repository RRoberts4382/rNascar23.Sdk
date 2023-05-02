using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Schedules.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Schedules.Ports
{
    public interface ISchedulesRepository : IJsonDataRepository
    {
        Task<SeriesSchedules> GetSchedulesAsync(
            int? year = null, 
            CancellationToken cancellationToken=default);

        Task<IList<SeriesEvent>> GetSchedulesAsync(
            ScheduleType scheduleType,
            int? year = null,
            CancellationToken cancellationToken = default);

        Task<SeriesEvent> GetEventAsync(
            int raceId,
            int? year = null,
            CancellationToken cancellationToken = default);
    }
}
