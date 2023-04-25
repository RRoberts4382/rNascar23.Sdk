namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class PitStop
    {
        public int PositionsGainedLost { get; set; }
        public float PitInElapsedTime { get; set; }
        public int PitInLapCount { get; set; }
        public int PitInLeaderLap { get; set; }
        public float PitOutElapsedTime { get; set; }
        public int PitInRank { get; set; }
        public int PitOutRank { get; set; }
    }
}
