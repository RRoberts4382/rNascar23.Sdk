using System.Collections.Generic;

namespace rNascar23.Sdk.Points.Models
{
    public class Stage
    {
        public int RaceId { get; set; }
        public int RunId { get; set; }
        public int StageNumber { get; set; }
        public IList<DriverStagePoints> DriverStagePoints { get; set; } = new List<DriverStagePoints>();
    }
}
