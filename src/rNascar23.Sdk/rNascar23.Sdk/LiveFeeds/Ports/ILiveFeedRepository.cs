using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.LiveFeeds.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.LiveFeeds.Ports
{
    public interface ILiveFeedRepository : IJsonDataRepository
    {
        Task<LiveFeed> GetLiveFeedAsync(CancellationToken cancellationToken = default);
    }
}
