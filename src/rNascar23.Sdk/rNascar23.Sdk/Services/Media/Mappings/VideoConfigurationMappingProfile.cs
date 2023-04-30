using AutoMapper;
using rNascar23.Sdk.Media.Models;
using rNascar23.Sdk.Services.Media.Data.Models;

namespace rNascar23.Sdk.Service.Media.Mappings
{
    internal class VideoConfigurationMappingProfile : Profile
    {
        public VideoConfigurationMappingProfile()
        {
            CreateMap<VideoConfigurationModel, VideoConfiguration>()
               .ForMember(m => m.Live, opts => opts.MapFrom(src => src.live))
               .ForMember(m => m.RaceID, opts => opts.MapFrom(src => src.raceId))
               .ForMember(m => m.DefaultDriverID, opts => opts.MapFrom(src => src.defaultDriverID))
               .ForMember(m => m.VideoComponents, opts => opts.MapFrom(src => src.data));

            CreateMap<VideoComponentModel, VideoComponent>()
               .ForMember(m => m.ComponentName, opts => opts.MapFrom(src => src.componentName))
               .ForMember(m => m.VideosChannels, opts => opts.MapFrom(src => src.videos));

            CreateMap<VideoChannelModel, VideoChannel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.DriverID, opt => opt.MapFrom(src => src.driverID))
                .ForMember(dest => dest.DriverOverlay, opt => opt.MapFrom(src => src.driverOverlay))
                .ForMember(dest => dest.DriverOverlayImage, opt => opt.MapFrom(src => src.driverOverlayImage))
                .ForMember(dest => dest.DriverOverlayName, opt => opt.MapFrom(src => src.driverOverlayName))
                .ForMember(dest => dest.Stream1, opt => opt.MapFrom(src => src.stream1))
                .ForMember(dest => dest.Stream1Is360, opt => opt.MapFrom(src => src.stream1Is360))
                .ForMember(dest => dest.Stream1AssetKey, opt => opt.MapFrom(src => src.stream1AssetKey))
                .ForMember(dest => dest.Stream1AssetKeyMobile, opt => opt.MapFrom(src => src.stream1AssetKeyMobile))
                .ForMember(dest => dest.Stream1IconText, opt => opt.MapFrom(src => src.stream1IconText))
                .ForMember(dest => dest.Stream1SponsorImage, opt => opt.MapFrom(src => src.stream1SponsorImage))
                .ForMember(dest => dest.Stream1SponsorName, opt => opt.MapFrom(src => src.stream1SponsorName))
                .ForMember(dest => dest.Stream2, opt => opt.MapFrom(src => src.stream2))
                .ForMember(dest => dest.Stream2Is360, opt => opt.MapFrom(src => src.stream2Is360))
                .ForMember(dest => dest.Stream2AssetKey, opt => opt.MapFrom(src => src.stream2AssetKey))
                .ForMember(dest => dest.Stream2AssetKeyMobile, opt => opt.MapFrom(src => src.stream2AssetKeyMobile))
                .ForMember(dest => dest.Stream2IconText, opt => opt.MapFrom(src => src.stream2IconText))
                .ForMember(dest => dest.Stream2SponsorImage, opt => opt.MapFrom(src => src.stream2SponsorImage))
                .ForMember(dest => dest.Stream2SponsorName, opt => opt.MapFrom(src => src.stream2SponsorName))
                .ForMember(dest => dest.Poster, opt => opt.MapFrom(src => src.poster))
                .ForMember(dest => dest.DriverManufacturer, opt => opt.MapFrom(src => src.driverManu))
                .ForMember(dest => dest.DriverVehicle, opt => opt.MapFrom(src => src.driverVehicle))
                .ForMember(dest => dest.DriverImage, opt => opt.MapFrom(src => src.driverImage));
        }
    }
}
