namespace rNascar23.Sdk.PitStops.Models
{
    public class PitStop
    {
        public string VehicleNumber { get; set; }
        public string DriverName { get; set; }
        public string VehicleManufacturer { get; set; }
        public int LeaderLap { get; set; }
        public int LapCount { get; set; }
        public int PitInFlagStatus { get; set; }
        public int PitOutFlagStatus { get; set; }
        public float PitInRaceTime { get; set; }
        public float PitOutRaceTime { get; set; }
        public float TotalDuration { get; set; }
        public float BoxStopRaceTime { get; set; }
        public float BoxLeaveRaceTime { get; set; }
        public float PitStopDuration { get; set; }
        public float InTravelDuration { get; set; }
        public float OutTravelDuration { get; set; }
        public string PitStopType { get; set; }
        public bool LeftFrontTireChanged { get; set; }
        public bool LeftRearTireChanged { get; set; }
        public bool RightFrontTireChanged { get; set; }
        public bool RightRearTireChanged { get; set; }
        public int PreviousLapTime { get; set; }
        public int NextLapTime { get; set; }
        public int PitInRank { get; set; }
        public int PitOutRank { get; set; }
        public int PositionsGainedLost { get; set; }
    }
}
