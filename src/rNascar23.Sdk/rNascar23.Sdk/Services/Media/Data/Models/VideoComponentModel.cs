using System.Collections.Generic;

namespace rNascar23.Sdk.Services.Media.Data.Models
{
    public class VideoComponentModel
    {
        public string componentName { get; set; }
        public IList<VideoChannelModel> videos { get; set; } = new List<VideoChannelModel>();
    }

}
