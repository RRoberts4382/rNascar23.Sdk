using System.Collections.Generic;

namespace rNascar23.Sdk.Media.Models
{
    public class VideoComponent
    {
        public string ComponentName { get; set; }
        public IList<VideoChannel> VideosChannels { get; set; } = new List<VideoChannel>();
    }

}
