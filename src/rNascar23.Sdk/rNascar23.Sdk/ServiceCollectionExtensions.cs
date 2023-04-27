using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Service.Flags;
using rNascar23.Sdk.Service.LapTimes;
using rNascar23.Sdk.Service.LiveFeeds;
using rNascar23.Sdk.Service.LoopData;
using rNascar23.Sdk.Service.Media;
using rNascar23.Sdk.Service.PitStops;
using rNascar23.Sdk.Service.Points;
using rNascar23.Sdk.Service.Schedules;
using rNascar23.Sdk.Service.Sources;
using System;

namespace rNascar23.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddrNascar23Sdk(this IServiceCollection services)
        {
            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddSources()
                .AddFlagState()
                .AddSchedules()
                .AddLiveFeed()
                .AddLapTimes()
                .AddPoints()
                .AddLoopData()
                .AddPitStops()
                .AddMedia();

            return services;
        }
    }
}
