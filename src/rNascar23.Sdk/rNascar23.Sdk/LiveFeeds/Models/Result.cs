using rNascar23.Sdk.Common;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public int FinishingPosition { get; set; }
        public int StartingPosition { get; set; }
        public string CarNumber { get; set; }
        public string DriverFullName { get; set; }
        public int DriverId { get; set; }
        public string DriverHometown { get; set; }
        public string HometownCity { get; set; }
        public string HometownState { get; set; }
        public string HometownCountry { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int QualifyingOrder { get; set; }
        public int QualifyingPosition { get; set; }
        public float QualifyingSpeed { get; set; }
        public int LapsLed { get; set; }
        public int TimesLed { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string Sponsor { get; set; }
        public int PointsEarned { get; set; }
        public int PlayoffPointsEarned { get; set; }
        public int LapsCompleted { get; set; }
        public string FinishingStatus { get; set; }
        public int Winnings { get; set; }
        public SeriesTypes SeriesId { get; set; }
        public int RaceSeason { get; set; }
        public int RaceId { get; set; }
        public string OwnerFullName { get; set; }
        public int CrewChiefId { get; set; }
        public string CrewChiefFullName { get; set; }
        public int PointsPosition { get; set; }
        public int PointsDelta { get; set; }
        public int OwnerId { get; set; }
        public string OfficialCarNumber { get; set; }
        public bool Disqualified { get; set; }
        public int DiffLaps { get; set; }
        public int DiffTime { get; set; }
    }
}
