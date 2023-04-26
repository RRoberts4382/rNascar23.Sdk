using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23.Sdk.Service.LapTimes.Adapters;

namespace rNascar23.Sdk.Service.LapTimes
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLapTimes(this IServiceCollection services)
        {
            services
                .AddTransient<IMoversFallersService, MoversFallersService>()
                .AddTransient<ILapTimesRepository, LapTimesRepository>()
                .AddTransient<ILapAveragesRepository, LapAveragesRepository>();

            return services;
        }
    }
}
