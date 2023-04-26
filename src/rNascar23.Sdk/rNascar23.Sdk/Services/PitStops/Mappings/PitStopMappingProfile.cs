using AutoMapper;
using rNascar23.Sdk.PitStops.Models;
using rNascar23.Sdk.Service.PitStops.Data.Models;

namespace rNascar23.Sdk.Service.PitStops.Mappings
{
    internal class PitStopMappingProfile : Profile
    {
        public PitStopMappingProfile()
        {
            CreateMap<PitStopModel, PitStop>()
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.vehicle_number))
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.driver_name))
                .ForMember(dest => dest.VehicleManufacturer, opt => opt.MapFrom(src => src.vehicle_manufacturer))
                .ForMember(dest => dest.LeaderLap, opt => opt.MapFrom(src => src.leader_lap))
                .ForMember(dest => dest.LapCount, opt => opt.MapFrom(src => src.lap_count))
                .ForMember(dest => dest.PitInFlagStatus, opt => opt.MapFrom(src => src.pit_in_flag_status))
                .ForMember(dest => dest.PitOutFlagStatus, opt => opt.MapFrom(src => src.pit_out_flag_status))
                .ForMember(dest => dest.PitInRaceTime, opt => opt.MapFrom(src => src.pit_in_race_time))
                .ForMember(dest => dest.PitOutRaceTime, opt => opt.MapFrom(src => src.pit_out_race_time))
                .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.total_duration))
                .ForMember(dest => dest.BoxStopRaceTime, opt => opt.MapFrom(src => src.box_stop_race_time))
                .ForMember(dest => dest.BoxLeaveRaceTime, opt => opt.MapFrom(src => src.box_leave_race_time))
                .ForMember(dest => dest.PitStopDuration, opt => opt.MapFrom(src => src.pit_stop_duration))
                .ForMember(dest => dest.InTravelDuration, opt => opt.MapFrom(src => src.in_travel_duration))
                .ForMember(dest => dest.OutTravelDuration, opt => opt.MapFrom(src => src.out_travel_duration))
                .ForMember(dest => dest.PitStopType, opt => opt.MapFrom(src => src.pit_stop_type))
                .ForMember(dest => dest.LeftFrontTireChanged, opt => opt.MapFrom(src => src.left_front_tire_changed))
                .ForMember(dest => dest.LeftRearTireChanged, opt => opt.MapFrom(src => src.left_rear_tire_changed))
                .ForMember(dest => dest.RightFrontTireChanged, opt => opt.MapFrom(src => src.right_front_tire_changed))
                .ForMember(dest => dest.RightRearTireChanged, opt => opt.MapFrom(src => src.right_rear_tire_changed))
                .ForMember(dest => dest.PreviousLapTime, opt => opt.MapFrom(src => src.previous_lap_time))
                .ForMember(dest => dest.NextLapTime, opt => opt.MapFrom(src => src.next_lap_time))
                .ForMember(dest => dest.PitInRank, opt => opt.MapFrom(src => src.pit_in_rank))
                .ForMember(dest => dest.PitOutRank, opt => opt.MapFrom(src => src.pit_out_rank))
                .ForMember(dest => dest.PositionsGainedLost, opt => opt.MapFrom(src => src.positions_gained_lost));
        }
    }
}
