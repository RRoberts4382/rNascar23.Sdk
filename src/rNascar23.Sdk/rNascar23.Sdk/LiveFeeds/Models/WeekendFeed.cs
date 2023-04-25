using System.Collections.Generic;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class WeekendFeed
    {
        public IList<WeekendRace> WeekendRaces { get; set; } = new List<WeekendRace>();
        public IList<WeekendRuns> WeekendRuns { get; set; } = new List<WeekendRuns>();
    }
}
