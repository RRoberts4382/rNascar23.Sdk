using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Flags.Ports;
using rNascar23.Sdk.Service.Flags.Adapters;

namespace rNascar23.Sdk.Service.Flags
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlagState(this IServiceCollection services)
        {
            services
                .AddTransient<IFlagStateRepository, FlagStateRepository>();

            return services;
        }
    }
}
