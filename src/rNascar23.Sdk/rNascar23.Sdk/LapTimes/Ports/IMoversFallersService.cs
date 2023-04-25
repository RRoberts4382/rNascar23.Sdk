using rNascar23.Sdk.LapTimes.Models;
using System.Collections.Generic;

namespace rNascar23.Sdk.LapTimes.Ports
{
    public interface IMoversFallersService
    {
        IList<PositionChange> GetDriverPositionChanges(LapTimeData lapTimeData);
    }
}
