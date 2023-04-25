namespace rNascar23.Sdk.Service.LiveFeeds.Data.Models
{
    internal class StageResultsModel
    {
        public int stage_number { get; set; }
        public DriverStageResultModel[] results { get; set; }
    }
}
