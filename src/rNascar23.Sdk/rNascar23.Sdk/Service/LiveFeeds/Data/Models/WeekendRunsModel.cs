using System;

namespace rNascar23.Sdk.Service.LiveFeeds.Data.Models
{
    internal class WeekendRunsModel
    {
        public int weekend_run_id { get; set; }
        public int race_id { get; set; }
        public int timing_run_id { get; set; }
        public int run_type { get; set; }
        public string run_name { get; set; }
        public DateTime run_date { get; set; }
        public DateTime run_date_utc { get; set; }
        public WeekendRunResultModel[] results { get; set; }
    }
}
