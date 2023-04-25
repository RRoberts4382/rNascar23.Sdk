namespace rNasar23Multi.Sdk.Data
{
    public interface IJsonDataRepository
    {
        string Url { get; }

        string Get(string url);
    }
}
