using AutoMapper;
using rNascar23.Sdk.Points.Models;
using rNascar23.Sdk.Service.Points.Data.Models;

namespace rNascar23.Sdk.Service.Points.Mappings
{
    internal class PointsMappingProfile : Profile
    {
        public PointsMappingProfile()
        {
            CreateMap<StageModel, Stage>()
                .ForMember(dest => dest.StageNumber, opt => opt.MapFrom(src => src.stage_number))
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.RunId, opt => opt.MapFrom(src => src.run_id))
                .ForMember(dest => dest.DriverStagePoints, opt => opt.MapFrom(src => src.results));

            CreateMap<StagePointsModel, DriverStagePoints>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.position))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.driver_id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.full_name))
                .ForMember(dest => dest.StagePoints, opt => opt.MapFrom(src => src.stage_points))
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.vehicle_number));

            CreateMap<StagePointsDetailsModel, StagePointsDetails>()
                .ForMember(dest => dest.BonusPoints, opt => opt.MapFrom(src => src.bonus_points))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.car_number))
                .ForMember(dest => dest.DeltaLeader, opt => opt.MapFrom(src => src.delta_leader))
                .ForMember(dest => dest.DeltaNext, opt => opt.MapFrom(src => src.delta_next))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.driver_id))
                .ForMember(dest => dest.IsInChase, opt => opt.MapFrom(src => src.is_in_chase))
                .ForMember(dest => dest.IsPointsEligible, opt => opt.MapFrom(src => src.is_points_eligible))
                .ForMember(dest => dest.IsRookie, opt => opt.MapFrom(src => src.is_rookie))
                .ForMember(dest => dest.MembershipId, opt => opt.MapFrom(src => src.membership_id))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.points))
                .ForMember(dest => dest.PointsPosition, opt => opt.MapFrom(src => src.points_position))
                .ForMember(dest => dest.PointsEarnedThisRace, opt => opt.MapFrom(src => src.points_earned_this_race))
                .ForMember(dest => dest.Stage1Points, opt => opt.MapFrom(src => src.stage_1_points))
                .ForMember(dest => dest.Stage1Winner, opt => opt.MapFrom(src => src.stage_1_winner))
                .ForMember(dest => dest.Stage2Points, opt => opt.MapFrom(src => src.stage_2_points))
                .ForMember(dest => dest.Stage2Winner, opt => opt.MapFrom(src => src.stage_2_winner))
                .ForMember(dest => dest.Stage3Points, opt => opt.MapFrom(src => src.stage_3_points))
                .ForMember(dest => dest.Stage3Winner, opt => opt.MapFrom(src => src.stage_3_winner))
                .ForMember(dest => dest.Wins, opt => opt.MapFrom(src => src.wins))
                .ForMember(dest => dest.Top5, opt => opt.MapFrom(src => src.top_5))
                .ForMember(dest => dest.Top10, opt => opt.MapFrom(src => src.top_10))
                .ForMember(dest => dest.Poles, opt => opt.MapFrom(src => src.poles))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.series_id))
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.RunId, opt => opt.MapFrom(src => src.run_id));

            CreateMap<DriverPointsModel, DriverPoints>()
                .ForMember(m => m.BonusPoints, opts => opts.MapFrom(src => src.bonus_points))
                .ForMember(m => m.CarNumber, opts => opts.MapFrom(src => src.car_number))
                .ForMember(m => m.DeltaLeader, opts => opts.MapFrom(src => src.delta_leader))
                .ForMember(m => m.DeltaNext, opts => opts.MapFrom(src => src.delta_next))
                .ForMember(m => m.DriverId, opts => opts.MapFrom(src => src.driver_id))
                .ForMember(m => m.FirstName, opts => opts.MapFrom(src => src.first_name))
                .ForMember(m => m.IsInChase, opts => opts.MapFrom(src => src.is_in_chase))
                .ForMember(m => m.IsPointsEligible, opts => opts.MapFrom(src => src.is_points_eligible))
                .ForMember(m => m.IsRookie, opts => opts.MapFrom(src => src.is_rookie))
                .ForMember(m => m.LastName, opts => opts.MapFrom(src => src.last_name))
                .ForMember(m => m.MembershipId, opts => opts.MapFrom(src => src.membership_id))
                .ForMember(m => m.Points, opts => opts.MapFrom(src => src.points))
                .ForMember(m => m.PointsEarnedThisRace, opts => opts.MapFrom(src => src.points_earned_this_race))
                .ForMember(m => m.PointsPosition, opts => opts.MapFrom(src => src.points_position))
                .ForMember(m => m.Poles, opts => opts.MapFrom(src => src.poles))
                .ForMember(m => m.RaceId, opts => opts.MapFrom(src => src.race_id))
                .ForMember(m => m.RunId, opts => opts.MapFrom(src => src.run_id))
                .ForMember(m => m.SeriesId, opts => opts.MapFrom(src => src.series_id))
                .ForMember(m => m.Stage1Points, opts => opts.MapFrom(src => src.stage_1_points))
                .ForMember(m => m.Stage1Winner, opts => opts.MapFrom(src => src.stage_1_winner))
                .ForMember(m => m.Stage2Points, opts => opts.MapFrom(src => src.stage_2_points))
                .ForMember(m => m.Stage2Winner, opts => opts.MapFrom(src => src.stage_2_winner))
                .ForMember(m => m.Stage3Points, opts => opts.MapFrom(src => src.stage_3_points))
                .ForMember(m => m.Stage3Winner, opts => opts.MapFrom(src => src.stage_3_winner))
                .ForMember(m => m.Top10, opts => opts.MapFrom(src => src.top_10))
                .ForMember(m => m.Top5, opts => opts.MapFrom(src => src.top_5))
                .ForMember(m => m.Wins, opts => opts.MapFrom(src => src.wins));
        }
    }
}
