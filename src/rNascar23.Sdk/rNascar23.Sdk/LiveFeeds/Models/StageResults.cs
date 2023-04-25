using System.Collections.Generic;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class StageResults
    {
        public int StageNumber { get; set; }
        public IList<DriverStageResult> Results { get; set; } = new List<DriverStageResult>();
    }
}
