using System.Threading.Tasks;

namespace rNasar23Multi.Sdk.Data
{
    public interface IJsonDataRepository
    {
        string Get(string url);
        Task<string> GetAsync(string url);
    }
}
