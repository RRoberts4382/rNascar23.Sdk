using rNascar23.Sdk.Common;
using System;

namespace rNascar23.Sdk.Schedules.Models
{
    public class EventActivity
    {
        public string EventName { get; set; }
        public string Notes { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime StartTimeLocal
        {
            get
            {
                return StartTimeUtc.ToLocalTime();
            }
        }
        public RunTypes RunType { get; set; }
        public string Description
        {
            get
            {
                return RunType == RunTypes.None ? String.Empty : RunType.ToString();
            }
        }
        public SeriesTypes SeriesId { get; set; }
    }
}
