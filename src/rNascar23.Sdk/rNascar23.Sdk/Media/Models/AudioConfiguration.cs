using System.Collections.Generic;

namespace rNascar23.Sdk.Media.Models
{
    public class AudioConfiguration
    {
        public int HistoricalRaceId { get; set; }
        public string RaceName { get; set; }
        public int RunType { get; set; }
        public string TrackName { get; set; }
        public int SeriesId { get; set; }
        public IList<AudioChannel> AudioChannels { get; set; } = new List<AudioChannel>();
    }
}
