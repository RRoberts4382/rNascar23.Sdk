﻿namespace rNascar23.Sdk.Service.Media.Models
{
    public class AudioChannelModel
    {
        public int stream_number { get; set; }
        public string driver_number { get; set; }
        public string driver_name { get; set; }
        public string base_url { get; set; }
        public string stream_rtmp { get; set; }
        public string stream_ios { get; set; }
        public bool requiresAuth { get; set; }
    }
}
