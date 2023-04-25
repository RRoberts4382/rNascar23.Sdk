using rNascar23.Sdk.Common;

namespace rNascar23.Sdk.LiveFeeds.Models
{
    public class CautionSegments
    {
        public int RaceId { get; set; }
        public int StartLap { get; set; }
        public int EndLap { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public string BeneficiaryCarNumber { get; set; }
        public FlagColors FlagState { get; set; }
    }
}
