using AutoMapper;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.Service.LapTimes.Data.Models;

namespace rNascar23.Sdk.Service.Flags.Mappings
{
    internal class LapTimesMappingProfile : Profile
    {
        public LapTimesMappingProfile()
        {
            CreateMap<LapAveragesModel, LapAverages>();


            CreateMap<FlagModel, LapFlag>()
                .ForMember(m => m.LapsCompleted, opts => opts.MapFrom(src => src.LapsCompleted))
                .ForMember(m => m.FlagState, opts => opts.MapFrom(src => src.FlagState));

            CreateMap<LapModel, LapDetails>()
                .ForMember(m => m.Lap, opts => opts.MapFrom(src => src.Lap))
                .ForMember(m => m.LapTime, opts => opts.MapFrom(src => src.LapTime))
                .ForMember(m => m.LapSpeed, opts => opts.MapFrom(src => src.LapSpeed))
                .ForMember(m => m.RunningPosition, opts => opts.MapFrom(src => src.RunningPos));

            CreateMap<DriverLapsModel, DriverLaps>()
                .ForMember(m => m.RunningPos, opts => opts.MapFrom(src => src.RunningPos))
                .ForMember(m => m.Number, opts => opts.MapFrom(src => src.Number))
                .ForMember(m => m.FullName, opts => opts.MapFrom(src => src.FullName))
                .ForMember(m => m.Manufacturer, opts => opts.MapFrom(src => src.Manufacturer))
                .ForMember(m => m.NASCARDriverID, opts => opts.MapFrom(src => src.NASCARDriverID))
                .ForMember(m => m.Laps, opts => opts.MapFrom(src => src.Laps));

            CreateMap<LapTimeDataModel, LapTimeData>()
                .ForMember(m => m.Drivers, opts => opts.MapFrom(src => src.Laps))
                .ForMember(m => m.LapFlags, opts => opts.MapFrom(src => src.Flags));
        }
    }
}
