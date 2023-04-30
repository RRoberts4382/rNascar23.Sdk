namespace rNascar23.Sdk.Media.Models
{
    public class VideoChannel
    {
        public int Id { get; set; }
        public int DriverID { get; set; }
        public int? PostID { get; set; }
        public string Title { get; set; }
        public bool DriverOverlay { get; set; }
        public string DriverOverlayImage { get; set; }
        public string DriverOverlayName { get; set; }
        public string Stream1 { get; set; }
        public bool Stream1Is360 { get; set; }
        public string Stream1AssetKey { get; set; }
        public string Stream1AssetKeyMobile { get; set; }
        public object Stream1IconText { get; set; }
        public string Stream1SponsorImage { get; set; }
        public string Stream1SponsorName { get; set; }
        public object Stream2 { get; set; }
        public bool Stream2Is360 { get; set; }
        public object Stream2AssetKey { get; set; }
        public object Stream2AssetKeyMobile { get; set; }
        public object Stream2IconText { get; set; }
        public object Stream2SponsorImage { get; set; }
        public object Stream2SponsorName { get; set; }
        public object Poster { get; set; }
        public string DriverManufacturer { get; set; }
        public string DriverVehicle { get; set; }
        public object DriverImage { get; set; }
    }
}
