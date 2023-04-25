namespace rNascar23.Sdk.Service.LiveFeeds.Data.Models
{
    internal class DriverModel
    {
        public int driver_id { get; set; }
        public string full_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool is_in_chase { get; set; }
    }
}
