namespace rNascar23.Sdk.Service.LiveFeeds.Data.Models
{
    internal class WeekendRunResultModel
    {
        public int run_id { get; set; }
        public string car_number { get; set; }
        public string vehicle_number { get; set; }
        public string manufacturer { get; set; }
        public int driver_id { get; set; }
        public string driver_name { get; set; }
        public int finishing_position { get; set; }
        public float best_lap_time { get; set; }
        public float best_lap_speed { get; set; }
        public int best_lap_number { get; set; }
        public int laps_completed { get; set; }
        public string comment { get; set; }
        public float delta_leader { get; set; }
        public bool disqualified { get; set; }
    }
}
