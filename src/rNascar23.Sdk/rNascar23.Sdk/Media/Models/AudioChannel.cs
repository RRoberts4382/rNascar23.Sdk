namespace rNascar23.Sdk.Media.Models
{
    public class AudioChannel
    {
        public int StreamNumber { get; set; }
        public string DriverNumber { get; set; }
        public string DriverName { get; set; }
        public string BaseUrl { get; set; }
        public string StreamRtmp { get; set; }
        public string StreamIos { get; set; }
        public bool RequiresAuthorization { get; set; }
        public string Source
        {
            get
            {
                return $"{BaseUrl}{StreamIos}";
            }
        }
    }
}
