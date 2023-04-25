using rNascar23.Sdk.Flags.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Flags.Ports
{
    public interface IFlagStateRepository
    {
        Task<IList<FlagState>> GetFlagStatesAsync();
    }
}
