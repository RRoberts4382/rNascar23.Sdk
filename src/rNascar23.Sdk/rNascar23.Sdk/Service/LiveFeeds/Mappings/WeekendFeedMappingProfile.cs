using AutoMapper;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;

namespace rNascar23.Sdk.Service.LiveFeeds.Mappings
{
    internal class WeekendFeedMappingProfile : Profile
    {
        public WeekendFeedMappingProfile()
        {
            CreateMap<WeekendFeedModel, WeekendFeed>()
                .ForMember(dest => dest.WeekendRaces, opt => opt.MapFrom(src => src.weekend_race))
                .ForMember(dest => dest.WeekendRuns, opt => opt.MapFrom(src => src.weekend_runs));

            CreateMap<WeekendRaceModel, WeekendRace>()
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => (SeriesTypes)src.series_id))
                .ForMember(dest => dest.RaceSeason, opt => opt.MapFrom(src => src.race_season))
                .ForMember(dest => dest.RaceName, opt => opt.MapFrom(src => src.race_name))
                .ForMember(dest => dest.RaceTypeId, opt => opt.MapFrom(src => (RaceTypes)src.race_type_id))
                .ForMember(dest => dest.RestrictorPlate, opt => opt.MapFrom(src => src.restrictor_plate))
                .ForMember(dest => dest.TrackId, opt => opt.MapFrom(src => src.track_id))
                .ForMember(dest => dest.TrackName, opt => opt.MapFrom(src => src.track_name))
                .ForMember(dest => dest.DateScheduled, opt => opt.MapFrom(src => src.date_scheduled))
                .ForMember(dest => dest.RaceDate, opt => opt.MapFrom(src => src.race_date))
                .ForMember(dest => dest.QualifyingDate, opt => opt.MapFrom(src => src.qualifying_date))
                .ForMember(dest => dest.TuneinDate, opt => opt.MapFrom(src => src.tunein_date))
                .ForMember(dest => dest.ScheduledDistance, opt => opt.MapFrom(src => src.scheduled_distance))
                .ForMember(dest => dest.ActualDistance, opt => opt.MapFrom(src => src.actual_distance))
                .ForMember(dest => dest.ScheduledLaps, opt => opt.MapFrom(src => src.scheduled_laps))
                .ForMember(dest => dest.ActualLaps, opt => opt.MapFrom(src => src.actual_laps))
                .ForMember(dest => dest.Stage1Laps, opt => opt.MapFrom(src => src.stage_1_laps))
                .ForMember(dest => dest.Stage2Laps, opt => opt.MapFrom(src => src.stage_2_laps))
                .ForMember(dest => dest.Stage3Laps, opt => opt.MapFrom(src => src.stage_3_laps))
                .ForMember(dest => dest.Stage4Laps, opt => opt.MapFrom(src => src.stage_4_laps))
                .ForMember(dest => dest.NumberOfCarsInField, opt => opt.MapFrom(src => src.number_of_cars_in_field))
                .ForMember(dest => dest.PoleWinnerDriverId, opt => opt.MapFrom(src => src.pole_winner_driver_id))
                .ForMember(dest => dest.PoleWinnerSpeed, opt => opt.MapFrom(src => src.pole_winner_speed))
                .ForMember(dest => dest.NumberOfLeadChanges, opt => opt.MapFrom(src => src.number_of_lead_changes))
                .ForMember(dest => dest.NumberOfLeaders, opt => opt.MapFrom(src => src.number_of_leaders))
                .ForMember(dest => dest.NumberOfCautions, opt => opt.MapFrom(src => src.number_of_cautions))
                .ForMember(dest => dest.NumberOfCautionLaps, opt => opt.MapFrom(src => src.number_of_caution_laps))
                .ForMember(dest => dest.AverageSpeed, opt => opt.MapFrom(src => src.average_speed))
                .ForMember(dest => dest.TotalRaceTime, opt => opt.MapFrom(src => src.total_race_time))
                .ForMember(dest => dest.MarginOfVictory, opt => opt.MapFrom(src => src.margin_of_victory))
                .ForMember(dest => dest.RacePurse, opt => opt.MapFrom(src => src.race_purse))
                .ForMember(dest => dest.RaceComments, opt => opt.MapFrom(src => src.race_comments))
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(src => src.attendance))
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.results))
                .ForMember(dest => dest.CautionSegments, opt => opt.MapFrom(src => src.caution_segments))
                .ForMember(dest => dest.RaceLeaders, opt => opt.MapFrom(src => src.race_leaders))
                .ForMember(dest => dest.Infractions, opt => opt.MapFrom(src => src.infractions))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.schedule))
                .ForMember(dest => dest.StageResults, opt => opt.MapFrom(src => src.stage_results))
                .ForMember(dest => dest.PitReports, opt => opt.MapFrom(src => src.pit_reports))
                .ForMember(dest => dest.RadioBroadcaster, opt => opt.MapFrom(src => src.radio_broadcaster))
                .ForMember(dest => dest.TelevisionBroadcaster, opt => opt.MapFrom(src => src.television_broadcaster))
                .ForMember(dest => dest.SatelliteRadioBroadcaster, opt => opt.MapFrom(src => src.satellite_radio_broadcaster))
                .ForMember(dest => dest.MasterRaceId, opt => opt.MapFrom(src => src.master_race_id))
                .ForMember(dest => dest.InspectionComplete, opt => opt.MapFrom(src => src.inspection_complete))
                .ForMember(dest => dest.PlayoffRound, opt => opt.MapFrom(src => src.playoff_round));

            CreateMap<WeekendRunsModel, WeekendRuns>()
                .ForMember(dest => dest.WeekendRunId, opt => opt.MapFrom(src => src.weekend_run_id))
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.TimingRunId, opt => opt.MapFrom(src => src.timing_run_id))
                .ForMember(dest => dest.RunType, opt => opt.MapFrom(src => (RunTypes)src.run_type))
                .ForMember(dest => dest.RunName, opt => opt.MapFrom(src => src.run_name))
                .ForMember(dest => dest.RunDate, opt => opt.MapFrom(src => src.run_date))
                .ForMember(dest => dest.RunDateUtc, opt => opt.MapFrom(src => src.run_date_utc))
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.results));

            CreateMap<ResultModel, Result>()
                .ForMember(dest => dest.FinishingPosition, opt => opt.MapFrom(src => src.finishing_position))
                .ForMember(dest => dest.StartingPosition, opt => opt.MapFrom(src => src.starting_position))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.car_number))
                .ForMember(dest => dest.DriverFullName, opt => opt.MapFrom(src => src.driver_fullname))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.driver_id))
                .ForMember(dest => dest.DriverHometown, opt => opt.MapFrom(src => src.driver_hometown))
                .ForMember(dest => dest.HometownCity, opt => opt.MapFrom(src => src.hometown_city))
                .ForMember(dest => dest.HometownState, opt => opt.MapFrom(src => src.hometown_state))
                .ForMember(dest => dest.HometownCountry, opt => opt.MapFrom(src => src.hometown_country))
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.team_id))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.team_name))
                .ForMember(dest => dest.QualifyingOrder, opt => opt.MapFrom(src => src.qualifying_order))
                .ForMember(dest => dest.QualifyingPosition, opt => opt.MapFrom(src => src.qualifying_position))
                .ForMember(dest => dest.QualifyingSpeed, opt => opt.MapFrom(src => src.qualifying_speed))
                .ForMember(dest => dest.LapsLed, opt => opt.MapFrom(src => src.laps_led))
                .ForMember(dest => dest.TimesLed, opt => opt.MapFrom(src => src.times_led))
                .ForMember(dest => dest.CarMake, opt => opt.MapFrom(src => src.car_make))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.car_model))
                .ForMember(dest => dest.Sponsor, opt => opt.MapFrom(src => src.sponsor))
                .ForMember(dest => dest.PointsEarned, opt => opt.MapFrom(src => src.points_earned))
                .ForMember(dest => dest.PlayoffPointsEarned, opt => opt.MapFrom(src => src.playoff_points_earned))
                .ForMember(dest => dest.LapsCompleted, opt => opt.MapFrom(src => src.laps_completed))
                .ForMember(dest => dest.FinishingStatus, opt => opt.MapFrom(src => src.finishing_status))
                .ForMember(dest => dest.Winnings, opt => opt.MapFrom(src => src.winnings))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => (SeriesTypes)src.series_id))
                .ForMember(dest => dest.RaceSeason, opt => opt.MapFrom(src => src.race_season))
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.OwnerFullName, opt => opt.MapFrom(src => src.owner_fullname))
                .ForMember(dest => dest.CrewChiefId, opt => opt.MapFrom(src => src.crew_chief_id))
                .ForMember(dest => dest.CrewChiefFullName, opt => opt.MapFrom(src => src.crew_chief_fullname))
                .ForMember(dest => dest.PointsPosition, opt => opt.MapFrom(src => src.points_position))
                .ForMember(dest => dest.PointsDelta, opt => opt.MapFrom(src => src.points_delta))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.owner_id))
                .ForMember(dest => dest.OfficialCarNumber, opt => opt.MapFrom(src => src.official_car_number))
                .ForMember(dest => dest.Disqualified, opt => opt.MapFrom(src => src.disqualified))
                .ForMember(dest => dest.DiffLaps, opt => opt.MapFrom(src => src.diff_laps))
                .ForMember(dest => dest.DiffTime, opt => opt.MapFrom(src => src.diff_time));

            CreateMap<CautionSegmentsModel, CautionSegments>()
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id))
                .ForMember(dest => dest.StartLap, opt => opt.MapFrom(src => src.start_lap))
                .ForMember(dest => dest.EndLap, opt => opt.MapFrom(src => src.end_lap))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.reason))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment))
                .ForMember(dest => dest.BeneficiaryCarNumber, opt => opt.MapFrom(src => src.beneficiary_car_number))
                .ForMember(dest => dest.FlagState, opt => opt.MapFrom(src => (FlagColors)src.flag_state));

            CreateMap<RaceLeadersModel, RaceLeaders>()
                .ForMember(dest => dest.StartLap, opt => opt.MapFrom(src => src.start_lap))
                .ForMember(dest => dest.EndLap, opt => opt.MapFrom(src => src.end_lap))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.car_number))
                .ForMember(dest => dest.RaceId, opt => opt.MapFrom(src => src.race_id));

            CreateMap<ScheduleModel, Schedule>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.event_name))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.notes))
                .ForMember(dest => dest.StartTimeUtc, opt => opt.MapFrom(src => src.start_time_utc))
                .ForMember(dest => dest.RunType, opt => opt.MapFrom(src => (RunTypes)src.run_type));

            CreateMap<StageResultsModel, StageResults>()
                .ForMember(dest => dest.StageNumber, opt => opt.MapFrom(src => src.stage_number))
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.results));

            CreateMap<DriverStageResultModel, DriverStageResult>()
                .ForMember(dest => dest.DriverFullname, opt => opt.MapFrom(src => src.driver_fullname))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.driver_id))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.car_number))
                .ForMember(dest => dest.FinishingPosition, opt => opt.MapFrom(src => src.finishing_position))
                .ForMember(dest => dest.StagePoints, opt => opt.MapFrom(src => src.stage_points));

            CreateMap<WeekendRunResultModel, WeekendRunResult>()
                .ForMember(dest => dest.RunId, opt => opt.MapFrom(src => src.run_id))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.car_number))
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.vehicle_number))
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.manufacturer))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.driver_id))
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.driver_name))
                .ForMember(dest => dest.FinishingPosition, opt => opt.MapFrom(src => src.finishing_position))
                .ForMember(dest => dest.BestLapTime, opt => opt.MapFrom(src => src.best_lap_time))
                .ForMember(dest => dest.BestLapSpeed, opt => opt.MapFrom(src => src.best_lap_speed))
                .ForMember(dest => dest.BestLapNumber, opt => opt.MapFrom(src => src.best_lap_number))
                .ForMember(dest => dest.LapsCompleted, opt => opt.MapFrom(src => src.laps_completed))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment))
                .ForMember(dest => dest.DeltaLeader, opt => opt.MapFrom(src => src.delta_leader))
                .ForMember(dest => dest.Disqualified, opt => opt.MapFrom(src => src.disqualified));
        }
    }
}
