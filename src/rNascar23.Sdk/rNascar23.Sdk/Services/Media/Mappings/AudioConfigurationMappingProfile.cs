using AutoMapper;
using rNascar23.Sdk.Media.Models;
using rNascar23.Sdk.Service.Media.Models;

namespace rNascar23.Sdk.Service.Media.Mappings
{
    internal class AudioConfigurationMappingProfile : Profile
    {
        public AudioConfigurationMappingProfile()
        {
            CreateMap<AudioConfigurationModel, AudioConfiguration>()
                .ForMember(m => m.HistoricalRaceId, opts => opts.MapFrom(src => src.historical_race_id))
                .ForMember(m => m.RaceName, opts => opts.MapFrom(src => src.race_name))
                .ForMember(m => m.RunType, opts => opts.MapFrom(src => src.run_type))
                .ForMember(m => m.TrackName, opts => opts.MapFrom(src => src.track_name))
                .ForMember(m => m.SeriesId, opts => opts.MapFrom(src => src.series_id))
                .ForMember(m => m.AudioChannels, opts => opts.MapFrom(src => src.audio_config));

            CreateMap<AudioChannelModel, AudioChannel>()
                .ForMember(dest => dest.StreamNumber, opt => opt.MapFrom(src => src.stream_number))
                .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(src => src.driver_number))
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.driver_name))
                .ForMember(dest => dest.BaseUrl, opt => opt.MapFrom(src => src.base_url))
                .ForMember(dest => dest.StreamRtmp, opt => opt.MapFrom(src => src.stream_rtmp))
                .ForMember(dest => dest.StreamIos, opt => opt.MapFrom(src => src.stream_ios))
                .ForMember(dest => dest.RequiresAuthorization, opt => opt.MapFrom(src => src.requiresAuth));
        }
    }
}
