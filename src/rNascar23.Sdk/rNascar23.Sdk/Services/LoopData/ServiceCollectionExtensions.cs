using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Service.LoopData.Adapters;
using rNascar23.Sdk.Service.Schedules;

namespace rNascar23.Sdk.Service.LoopData
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoopData(this IServiceCollection services)
        {
            services
                .AddSchedules()
                .AddTransient<IDriverInfoRepository, DriverInfoRepository>()
                .AddTransient<ILoopDataRepository, LoopDataRepository>();

            return services;
        }
    }
}
