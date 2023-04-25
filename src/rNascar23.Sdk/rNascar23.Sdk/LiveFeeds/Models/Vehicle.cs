using rNascar23.Sdk.Common;
using System.Collections.Generic;
using System.Linq;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class Vehicle
    {
        public float AverageRestartSpeed { get; set; }
        public float AverageRunningPosition { get; set; }
        public float AverageSpeed { get; set; }
        public int BestLap { get; set; }
        public float BestLapSpeed { get; set; }
        public float BestLapTime { get; set; }
        public string VehicleManufacturer { get; set; }
        public string VehicleNumber { get; set; }
        public Driver Driver { get; set; }
        public float VehicleElapsedTime { get; set; }
        public int FastestLapsRun { get; set; }
        public int LapsPositionImproved { get; set; }
        public int LapsCompleted { get; set; }
        public IList<LapsLed> LapsLed { get; set; } = new List<LapsLed>();
        public float LastLapSpeed { get; set; }
        public float LastLapTime { get; set; }
        public int PassesMade { get; set; }
        public int PassingDifferential { get; set; }
        public int PositionDifferentialLast10Percent { get; set; }
        public IList<PitStop> PitStops { get; set; } = new List<PitStop>();
        public int QualifyingStatus { get; set; }
        public int RunningPosition { get; set; }
        public VehicleStatusTypes Status { get; set; }
        public float Delta { get; set; }
        public string SponsorName { get; set; }
        public int StartingPosition { get; set; }
        public int TimesPassed { get; set; }
        public int QualityPasses { get; set; }
        public bool IsOnTrack { get; set; }
        public bool IsOnDvp { get; set; }
        public int? LastPit
        {
            get
            {
                if (PitStops.Count > 0)
                {
                    var lastStop = PitStops.OrderBy(p => p.PitInLeaderLap).LastOrDefault();
                    return lastStop.PitInLeaderLap;
                }

                return null;
            }
        }
    }
}
