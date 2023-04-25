using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Media.Ports;

namespace rNascar23.Sdk.Service.Media
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMedia(this IServiceCollection services)
        {
            services
                .AddSingleton<IMediaRepository, MediaRepository>();

            return services;
        }
    }
}
