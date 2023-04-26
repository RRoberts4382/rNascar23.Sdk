using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Points.Ports;
using rNascar23.Sdk.Service.Points.Adapters;

namespace rNascar23.Sdk.Service.Points
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPoints(this IServiceCollection services)
        {
            services
                .AddTransient<IPointsRepository, PointsRepository>();

            return services;
        }
    }
}
