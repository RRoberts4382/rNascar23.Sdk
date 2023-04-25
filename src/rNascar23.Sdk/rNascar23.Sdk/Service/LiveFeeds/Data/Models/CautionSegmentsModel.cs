namespace rNascar23.Sdk.Service.LiveFeeds.Data.Models
{
    internal class CautionSegmentsModel
    {
        public int race_id { get; set; }
        public int start_lap { get; set; }
        public int end_lap { get; set; }
        public string reason { get; set; }
        public string comment { get; set; }
        public string beneficiary_car_number { get; set; }
        public int flag_state { get; set; }
    }
}
