using System.Collections.Generic;

namespace rNascar23.Sdk.Services.Media.Data.Models
{
    internal class VideoConfigurationModel
    {
        public bool live { get; set; }
        public int raceId { get; set; }
        public int defaultDriverID { get; set; }
        public IList<VideoComponentModel> data { get; set; } = new List<VideoComponentModel>();
    }
}
