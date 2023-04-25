using rNascar23.Sdk.LiveFeeds.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Data.LiveFeeds.Ports
{
    public interface ILiveFeedRepository
    {
        Task<LiveFeed> GetLiveFeedAsync(CancellationToken cancellationToken = default);
    }
}
