using AutoMapper;
using rNascar23.Sdk.LoopData.Models;
using rNascar23.Sdk.Service.LoopData.Data.Models;

namespace rNascar23.Sdk.Service.LoopData.Mappings
{
    internal class LoopDataMappingProfile : Profile
    {
        public LoopDataMappingProfile()
        {
            CreateMap<EventLoopDataModel, EventLoopData>()
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.RaceName, opt => opt.MapFrom(src => src.race_name))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.series_id))
                .ForMember(dest => dest.ScheduledLaps, opt => opt.MapFrom(src => src.sch_laps))
                .ForMember(dest => dest.ActualLaps, opt => opt.MapFrom(src => src.act_laps))
                .ForMember(dest => dest.Drivers, opt => opt.MapFrom(src => src.drivers));

            CreateMap<DriverModel, DriverLoopData>()
                  .ForMember(m => m.AveragePosition, opts => opts.MapFrom(src => src.avg_ps))
                  .ForMember(m => m.BestPosition, opts => opts.MapFrom(src => src.best_ps))
                  .ForMember(m => m.ClosingLapsDifference, opts => opts.MapFrom(src => src.closing_laps_diff))
                  .ForMember(m => m.ClosingPosition, opts => opts.MapFrom(src => src.closing_ps))
                  .ForMember(m => m.DriverId, opts => opts.MapFrom(src => src.driver_id))
                  .ForMember(m => m.FastestLaps, opts => opts.MapFrom(src => src.fast_laps))
                  .ForMember(m => m.Laps, opts => opts.MapFrom(src => src.laps))
                  .ForMember(m => m.LeadLaps, opts => opts.MapFrom(src => src.lead_laps))
                  .ForMember(m => m.MidPosition, opts => opts.MapFrom(src => src.mid_ps))
                  .ForMember(m => m.PassedGreenFlag, opts => opts.MapFrom(src => src.passed_gf))
                  .ForMember(m => m.PassesGreenFlag, opts => opts.MapFrom(src => src.passes_gf))
                  .ForMember(m => m.PassingDifference, opts => opts.MapFrom(src => src.passing_diff))
                  .ForMember(m => m.Position, opts => opts.MapFrom(src => src.ps))
                  .ForMember(m => m.QualityPasses, opts => opts.MapFrom(src => src.quality_passes))
                  .ForMember(m => m.Rating, opts => opts.MapFrom(src => src.rating))
                  .ForMember(m => m.StartPosition, opts => opts.MapFrom(src => src.start_ps))
                  .ForMember(m => m.Top15Laps, opts => opts.MapFrom(src => src.top15_laps))
                  .ForMember(m => m.WorstPosition, opts => opts.MapFrom(src => src.worst_ps));
        }
    }
}
