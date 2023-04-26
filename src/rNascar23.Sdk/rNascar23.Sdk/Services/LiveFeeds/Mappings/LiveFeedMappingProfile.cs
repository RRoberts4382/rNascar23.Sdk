using AutoMapper;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.Service.LiveFeeds.Data.Models;

namespace rNascar23.Sdk.LiveFeeds.Mappings
{
    internal class LiveFeedMappingProfile : Profile
    {
        public LiveFeedMappingProfile()
        {
            CreateMap<PitStopModel, PitStop>()
                .ForMember(dest => dest.PositionsGainedLost, opt => opt.MapFrom(src => src.positions_gained_lossed))
                .ForMember(dest => dest.PitInElapsedTime, opt => opt.MapFrom(src => src.pit_in_elapsed_time))
                .ForMember(dest => dest.PitInLapCount, opt => opt.MapFrom(src => src.pit_in_lap_count))
                .ForMember(dest => dest.PitInLeaderLap, opt => opt.MapFrom(src => src.pit_in_leader_lap))
                .ForMember(dest => dest.PitOutElapsedTime, opt => opt.MapFrom(src => src.pit_out_elapsed_time))
                .ForMember(dest => dest.PitInRank, opt => opt.MapFrom(src => src.pit_in_rank))
                .ForMember(dest => dest.PitOutRank, opt => opt.MapFrom(src => src.pit_out_rank));

            CreateMap<DriverModel, Driver>()
                .ForMember(m => m.DriverId, opts => opts.MapFrom(src => (long)src.driver_id))
                .ForMember(m => m.IsInChase, opts => opts.MapFrom(src => src.is_in_chase))
                .ForMember(m => m.FirstName, opts => opts.MapFrom(src => src.first_name))
                .ForMember(m => m.LastName, opts => opts.MapFrom(src => src.last_name))
                .ForMember(m => m.FullName, opts => opts.MapFrom(src => src.full_name));

            CreateMap<LapsLedModel, LapsLed>()
                .ForMember(m => m.StartLap, opts => opts.MapFrom(src => src.start_lap))
                .ForMember(m => m.EndLap, opts => opts.MapFrom(src => src.end_lap));

            CreateMap<VehicleModel, Vehicle>()
                .ForMember(dest => dest.AverageRestartSpeed, opt => opt.MapFrom(src => src.average_restart_speed))
                .ForMember(dest => dest.AverageRunningPosition, opt => opt.MapFrom(src => src.average_running_position))
                .ForMember(dest => dest.AverageSpeed, opt => opt.MapFrom(src => src.average_speed))
                .ForMember(dest => dest.BestLap, opt => opt.MapFrom(src => src.best_lap))
                .ForMember(dest => dest.BestLapSpeed, opt => opt.MapFrom(src => src.best_lap_speed))
                .ForMember(dest => dest.BestLapTime, opt => opt.MapFrom(src => src.best_lap_time))
                .ForMember(dest => dest.VehicleManufacturer, opt => opt.MapFrom(src => src.vehicle_manufacturer))
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.vehicle_number))
                .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => src.driver))
                .ForMember(dest => dest.VehicleElapsedTime, opt => opt.MapFrom(src => src.vehicle_elapsed_time))
                .ForMember(dest => dest.FastestLapsRun, opt => opt.MapFrom(src => src.fastest_laps_run))
                .ForMember(dest => dest.LapsPositionImproved, opt => opt.MapFrom(src => src.laps_position_improved))
                .ForMember(dest => dest.LapsCompleted, opt => opt.MapFrom(src => src.laps_completed))
                .ForMember(dest => dest.LapsLed, opt => opt.MapFrom(src => src.laps_led))
                .ForMember(dest => dest.LastLapSpeed, opt => opt.MapFrom(src => src.last_lap_speed))
                .ForMember(dest => dest.LastLapTime, opt => opt.MapFrom(src => src.last_lap_time))
                .ForMember(dest => dest.PassesMade, opt => opt.MapFrom(src => src.passes_made))
                .ForMember(dest => dest.PassingDifferential, opt => opt.MapFrom(src => src.passing_differential))
                .ForMember(dest => dest.PositionDifferentialLast10Percent, opt => opt.MapFrom(src => src.position_differential_last_10_percent))
                .ForMember(dest => dest.PitStops, opt => opt.MapFrom(src => src.pit_stops))
                .ForMember(dest => dest.QualifyingStatus, opt => opt.MapFrom(src => src.qualifying_status))
                .ForMember(dest => dest.RunningPosition, opt => opt.MapFrom(src => src.running_position))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Delta, opt => opt.MapFrom(src => src.delta))
                .ForMember(dest => dest.SponsorName, opt => opt.MapFrom(src => src.sponsor_name))
                .ForMember(dest => dest.StartingPosition, opt => opt.MapFrom(src => src.starting_position))
                .ForMember(dest => dest.TimesPassed, opt => opt.MapFrom(src => src.times_passed))
                .ForMember(dest => dest.QualityPasses, opt => opt.MapFrom(src => src.quality_passes))
                .ForMember(dest => dest.IsOnTrack, opt => opt.MapFrom(src => src.is_on_track))
                .ForMember(dest => dest.IsOnDvp, opt => opt.MapFrom(src => src.is_on_dvp))
                .ForMember(dest => dest.LastPit, opt => opt.Ignore());

            CreateMap<StageModel, Stage>()
                .ForMember(m => m.Number, opts => opts.MapFrom(src => (long)src.stage_num))
                .ForMember(m => m.FinishAtLap, opts => opts.MapFrom(src => src.finish_at_lap))
                .ForMember(m => m.LapsInStage, opts => opts.MapFrom(src => src.laps_in_stage))
                .ReverseMap();

            CreateMap<LiveFeedModel, LiveFeed>()
                .ForMember(m => m.AverageDifference1To3, opts => opts.MapFrom(src => src.avg_diff_1to3))
                .ForMember(m => m.ElapsedTime, opts => opts.MapFrom(src => src.elapsed_time))
                .ForMember(m => m.FlagState, opts => opts.MapFrom(src => src.flag_state))
                .ForMember(m => m.LapNumber, opts => opts.MapFrom(src => src.lap_number))
                .ForMember(m => m.LapsInRace, opts => opts.MapFrom(src => src.laps_in_race))
                .ForMember(m => m.LapsToGo, opts => opts.MapFrom(src => src.laps_to_go))
                .ForMember(m => m.NumberOfCautionLaps, opts => opts.MapFrom(src => src.number_of_caution_laps))
                .ForMember(m => m.NumberOfCautionSegments, opts => opts.MapFrom(src => src.number_of_caution_segments))
                .ForMember(m => m.NumberOfLeadChanges, opts => opts.MapFrom(src => src.number_of_lead_changes))
                .ForMember(m => m.NumberOfLeaders, opts => opts.MapFrom(src => src.number_of_leaders))
                .ForMember(m => m.RaceId, opts => opts.MapFrom(src => src.race_id))
                .ForMember(m => m.RunId, opts => opts.MapFrom(src => src.run_id))
                .ForMember(m => m.RunName, opts => opts.MapFrom(src => src.run_name))
                .ForMember(m => m.RunType, opts => opts.MapFrom(src => src.run_type))
                .ForMember(m => m.SeriesId, opts => opts.MapFrom(src => src.series_id))
                .ForMember(m => m.Stage, opts => opts.MapFrom(src => src.stage))
                .ForMember(m => m.TimeOfDay, opts => opts.MapFrom(src => src.time_of_day))
                .ForMember(m => m.TimeOfDayOs, opts => opts.MapFrom(src => src.time_of_day_os))
                .ForMember(m => m.TrackId, opts => opts.MapFrom(src => src.track_id))
                .ForMember(m => m.TrackLength, opts => opts.MapFrom(src => src.track_length))
                .ForMember(m => m.TrackName, opts => opts.MapFrom(src => src.track_name))
                .ForMember(m => m.Vehicles, opts => opts.MapFrom(src => src.vehicles));
        }
    }
}