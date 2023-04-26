using rNascar23.Sdk.Common;
using System.Collections.Generic;

namespace rNascar23.Sdk.LoopData.Models
{
    public class EventLoopData
    {
        public int RaceId { get; set; }
        public string RaceName { get; set; }
        public SeriesTypes SeriesId { get; set; }
        public int ScheduledLaps { get; set; }
        public int ActualLaps { get; set; }
        public IList<DriverLoopData> Drivers { get; set; }
    }
}
