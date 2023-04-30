using Microsoft.Extensions.Logging;

namespace rNascar23.Sdk.Data
{
    internal abstract class ResettableCircuitBreakerRepository : JsonDataRepository
    {
        private int _lastRaceId;

        protected ResettableCircuitBreakerRepository(ILogger<JsonDataRepository> logger) : base(logger)
        {
        }

        protected virtual void CheckForNewRaceId(int raceId)
        {
            if (CircuitBreakerTripped && raceId != _lastRaceId)
            {
                ResetCircuitBreaker();
            }

            _lastRaceId = raceId;
        }
    }
}
