using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Schedules.Ports;
using rNascar23.Sdk.Service.Schedules.Adapters;

namespace rNascar23.Sdk.Service.Schedules
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSchedules(this IServiceCollection services)
        {
            services
                .AddTransient<ISchedulesRepository, SchedulesRepository>();

            return services;
        }
    }
}
