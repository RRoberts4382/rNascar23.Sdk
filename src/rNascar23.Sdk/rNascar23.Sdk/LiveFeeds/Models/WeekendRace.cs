using rNascar23.Sdk.Common;
using System;
using System.Collections.Generic;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class WeekendRace
    {
        public int RaceId { get; set; }
        public SeriesTypes SeriesId { get; set; }
        public int RaceSeason { get; set; }
        public string RaceName { get; set; }
        public RaceTypes RaceTypeId { get; set; }
        public bool RestrictorPlate { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public DateTime DateScheduled { get; set; }
        public DateTime RaceDate { get; set; }
        public DateTime QualifyingDate { get; set; }
        public DateTime TuneinDate { get; set; }
        public float ScheduledDistance { get; set; }
        public float ActualDistance { get; set; }
        public int ScheduledLaps { get; set; }
        public int ActualLaps { get; set; }
        public int Stage1Laps { get; set; }
        public int Stage2Laps { get; set; }
        public int Stage3Laps { get; set; }
        public int Stage4Laps { get; set; }
        public int NumberOfCarsInField { get; set; }
        public int PoleWinnerDriverId { get; set; }
        public float PoleWinnerSpeed { get; set; }
        public int NumberOfLeadChanges { get; set; }
        public int NumberOfLeaders { get; set; }
        public int NumberOfCautions { get; set; }
        public int NumberOfCautionLaps { get; set; }
        public float AverageSpeed { get; set; }
        public string TotalRaceTime { get; set; }
        public string MarginOfVictory { get; set; }
        public int RacePurse { get; set; }
        public string RaceComments { get; set; }
        public int Attendance { get; set; }
        public IList<Result> Results { get; set; } = new List<Result>();
        public IList<CautionSegments> CautionSegments { get; set; } = new List<CautionSegments>();
        public IList<RaceLeaders> RaceLeaders { get; set; } = new List<RaceLeaders>();
        public IList<object> Infractions { get; set; } = new List<object>();
        public IList<Schedule> Schedule { get; set; } = new List<Schedule>();
        public IList<StageResults> StageResults { get; set; } = new List<StageResults>();
        public IList<object> PitReports { get; set; } = new List<object>();
        public string RadioBroadcaster { get; set; }
        public string TelevisionBroadcaster { get; set; }
        public string SatelliteRadioBroadcaster { get; set; }
        public int MasterRaceId { get; set; }
        public bool InspectionComplete { get; set; }
        public int PlayoffRound { get; set; }
    }
}
