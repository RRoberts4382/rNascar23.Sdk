using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace rNascar23.Sdk.Sources.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApiSourceParameterType
    {
        Season = 0,
        SeriesId = 1,
        RaceId = 2
    }
}
