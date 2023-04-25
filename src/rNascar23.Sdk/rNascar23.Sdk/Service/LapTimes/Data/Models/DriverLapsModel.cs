namespace rNascar23.Sdk.Service.LapTimes.Data.Models
{
    internal class DriverLapsModel
    {
        public string Number { get; set; }
        public string FullName { get; set; }
        public string Manufacturer { get; set; }
        public int? RunningPos { get; set; }
        public int? NASCARDriverID { get; set; }
        public LapModel[] Laps { get; set; }
    }
}
