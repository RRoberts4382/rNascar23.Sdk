using System.Collections.Generic;

namespace rNascar23.Sdk.Service.Media.Models
{
    public class AudioConfigurationModel
    {
        public int historical_race_id { get; set; }
        public string race_name { get; set; }
        public int run_type { get; set; }
        public string track_name { get; set; }
        public int series_id { get; set; }
        public IList<AudioChannelModel> audio_config { get; set; } = new List<AudioChannelModel>();
    }
}
