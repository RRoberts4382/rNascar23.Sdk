using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Services.Sources.Adapters;
using rNascar23.Sdk.Sources.Ports;

namespace rNascar23.Sdk.Service.Sources
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSources(this IServiceCollection services)
        {
            services
                .AddTransient<IApiSourcesRepository, ApiSourcesRepository>();

            return services;
        }
    }
}
