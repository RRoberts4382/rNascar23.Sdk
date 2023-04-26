using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.PitStops.Ports;
using rNascar23.Sdk.Service.PitStops.Adapters;

namespace rNascar23.Sdk.Service.PitStops
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPitStops(this IServiceCollection services)
        {
            services
                .AddTransient<IPitStopsRepository, PitStopsRepository>();

            return services;
        }
    }
}