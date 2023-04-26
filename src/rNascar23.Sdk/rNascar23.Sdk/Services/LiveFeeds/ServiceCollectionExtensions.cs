using Microsoft.Extensions.DependencyInjection;
using rNascar23.Sdk.Data.LiveFeeds.Ports;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Service.LiveFeeds.Adapters;

namespace rNascar23.Sdk.Service.LiveFeeds
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiveFeed(this IServiceCollection services)
        {
            services
                .AddTransient<IKeyMomentsRepository, KeyMomentsRepository>()
                .AddTransient<IWeekendFeedRepository, WeekendFeedRepository>()
                .AddTransient<ILiveFeedRepository, LiveFeedRepository>();

            return services;
        }
    }
}
