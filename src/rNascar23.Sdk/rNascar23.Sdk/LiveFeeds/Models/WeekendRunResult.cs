namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class WeekendRunResult
    {
        public int RunId { get; set; }
        public string CarNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string Manufacturer { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public int FinishingPosition { get; set; }
        public float BestLapTime { get; set; }
        public float BestLapSpeed { get; set; }
        public int BestLapNumber { get; set; }
        public int LapsCompleted { get; set; }
        public string Comment { get; set; }
        public float DeltaLeader { get; set; }
        public bool Disqualified { get; set; }
    }
}
