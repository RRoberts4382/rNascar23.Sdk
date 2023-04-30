namespace rNasar23Multi.Sdk.Data
{
    public interface IJsonDataRepository
    {
        bool CircuitBreakerTripped { get; }

        void ResetCircuitBreaker();
    }
}
