using rNascar23.Sdk.Common;
using System;
using System.Collections.Generic;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class WeekendRuns
    {
        public int WeekendRunId { get; set; }
        public int RaceId { get; set; }
        public int TimingRunId { get; set; }
        public RunTypes RunType { get; set; }
        public string RunName { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime RunDateUtc { get; set; }
        public IList<WeekendRunResult> Results { get; set; } = new List<WeekendRunResult>();
    }
}
