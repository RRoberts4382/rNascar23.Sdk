using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace rNascar23.Sdk.Sources.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApiSourceType
    {
        FlagState,
        LapTimes,
        LapAverages,
        KeyMoments,
        LiveFeed,
        WeekendFeed,
        LoopData,
        PitStops,
        Points,
        Schedules
    }
}
