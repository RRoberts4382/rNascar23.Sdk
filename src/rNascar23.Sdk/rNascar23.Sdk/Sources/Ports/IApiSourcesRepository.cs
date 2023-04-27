using rNascar23.Sdk.Sources.Models;
using System.Collections.Generic;

namespace rNascar23.Sdk.Sources.Ports
{
    public interface IApiSourcesRepository
    {
        IEnumerable<ApiSource> GetApiSources();

        ApiSource GetApiSource(ApiSourceType sourceType);

        string GetApiUrl(
            ApiSourceType sourceType,
            int? season = null,
            int? seriesId = null,
            int? raceId = null);
    }
}
