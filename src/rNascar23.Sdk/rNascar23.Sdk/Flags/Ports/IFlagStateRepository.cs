using rNascar23.Sdk.Flags.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Flags.Ports
{
    public interface IFlagStateRepository
    {
        Task<IEnumerable<FlagState>> GetFlagStatesAsync(
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
