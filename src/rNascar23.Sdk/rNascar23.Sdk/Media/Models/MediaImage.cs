using rNascar23.Sdk.Common;

namespace rNascar23.Sdk.Media.Models
{
    public class MediaImage
    {
        public MediaTypes MediaType { get; set; }
        public SeriesTypes SeriesId { get; set; }
        public int CarNumber { get; set; }
        public byte[] Image { get; set; }
    }
}
