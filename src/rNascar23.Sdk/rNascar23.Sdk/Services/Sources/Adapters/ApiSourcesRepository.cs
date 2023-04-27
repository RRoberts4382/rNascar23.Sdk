using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Services.Sources.Adapters
{
    /// <summary>
    /// Sources Repository
    /// Contains the urls for the NASCAR data endpoints.
    /// Note: Call LiveFeed first, to get the current SeriesId and RaceId.
    /// You can use these to call the other endpoints.
    /// SeriesIds are 1=Cup, 2=Xfinity, 3=Trucks
    /// You may occasionally see data from SeriesId 9999, typically Arca or Whelen Tour Mods.
    /// 
    /// The purpose of this repository and associated json file is to provide a way
    /// for users to update the addresses of the endpoints if they ever change in the future,
    /// without having to change the actual code in the application.
    /// Just update the ApiSources.json file in the rNascar23\Data\ directory in MyDocuments.
    /// 
    /// </summary>
    internal class ApiSourcesRepository : IApiSourcesRepository
    {
        #region consts

        protected const string DataFileName = "ApiSources.json";

        #endregion

        #region fields

        protected readonly ILogger<ApiSourcesRepository> _logger;

        #endregion

        #region ctor

        public ApiSourcesRepository(ILogger<ApiSourcesRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region public

        public virtual IEnumerable<ApiSource> GetApiSources()
        {
            IEnumerable<ApiSource> apiSources;

            try
            {
                apiSources = LoadApiSourceData();

                if (apiSources.Count() == 0)
                {
                    apiSources = CreateDefaultData();
                }

                return apiSources;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading api source data");
            }

            return new List<ApiSource>();
        }

        public virtual ApiSource GetApiSource(ApiSourceType sourceType)
        {
            try
            {
                var apiSources = LoadApiSourceData();

                if (apiSources.Count() == 0)
                {
                    apiSources = CreateDefaultData();
                }

                return apiSources.FirstOrDefault(s => s.SourceType == sourceType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading api source {sourceType}");
            }

            return null;
        }

        public virtual string GetApiUrl(
            ApiSourceType sourceType,
            int? season = null,
            int? seriesId = null,
            int? raceId = null)
        {
            try
            {
                var apiSource = GetApiSource(sourceType);

                if (apiSource == null)
                    throw new ArgumentException($"No Api Source found for {sourceType}");

                if (apiSource.UrlParameters.Count == 0)
                    return apiSource.UrlTemplate;

                string[] urlParams = new string[apiSource.UrlParameters.Count];

                for (int i = 0; i < apiSource.UrlParameters.Count; i++)
                {
                    var urlParam = apiSource.UrlParameters[i];

                    urlParams[i] = urlParam == ApiSourceParameterType.Season ?
                            season.GetValueOrDefault(DateTime.Now.Year).ToString() :
                        urlParam == ApiSourceParameterType.SeriesId ?
                            seriesId.GetValueOrDefault(1).ToString() :
                        urlParam == ApiSourceParameterType.RaceId ?
                            raceId.GetValueOrDefault(0).ToString() :
                        String.Empty;
                }

                var builtUrl = String.Format(apiSource.UrlTemplate, urlParams);

                return builtUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error building api source url for {sourceType}");
            }

            return string.Empty;
        }

        #endregion

        #region protected

        protected virtual IList<ApiSource> LoadApiSourceData()
        {
            var fileName = BuildDataFilePath();

            if (!File.Exists(fileName))
            {
                return new List<ApiSource>();
            }

            var json = File.ReadAllText(fileName);

            if (string.IsNullOrEmpty(json))
            {
                return new List<ApiSource>();
            }

            var data = JsonConvert.DeserializeObject<List<ApiSource>>(json);

            return data;
        }

        protected virtual IList<ApiSource> CreateDefaultData()
        {
            var defaultApiSources = BuildDefaultApiSourceData();

            var fileName = BuildDataFilePath();

            if (!File.Exists(fileName))
            {
                var json = JsonConvert.SerializeObject(defaultApiSources, Formatting.Indented);

                File.WriteAllText(fileName, json);
            }

            return defaultApiSources;
        }

        protected virtual IList<ApiSource> BuildDefaultApiSourceData()
        {
            IList<ApiSource> apiSources = new List<ApiSource>();

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.FlagState,
                UrlTemplate = @"https://cf.nascar.com/live/feeds/live-flag-data.json",
                UrlExample = @"https://cf.nascar.com/live/feeds/live-flag-data.json"
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.LapTimes,
                UrlTemplate = @"https://cf.nascar.com/cacher/{0}/{1}/{2}/lap-times.json",
                UrlExample = @"https://cf.nascar.com/cacher/2023/2/5314/lap-times.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.Season },
                    {1, ApiSourceParameterType.SeriesId },
                    {2, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.LapAverages,
                UrlTemplate = @"https://cf.nascar.com/cacher/{0}/{1}/{2}/lap-averages.json",
                UrlExample = @"https://cf.nascar.com/cacher/2023/2/5314/lap-averages.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.Season },
                    {1, ApiSourceParameterType.SeriesId },
                    {2, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.KeyMoments,
                UrlTemplate = @"https://cf.nascar.com/cacher/{0}/{1}/{2}/lap-notes.json",
                UrlExample = @"https://cf.nascar.com/cacher/2023/2/5314/lap-notes.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.Season },
                    {1, ApiSourceParameterType.SeriesId },
                    {2, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.LiveFeed,
                UrlTemplate = @"https://cf.nascar.com/live/feeds/live-feed.json",
                UrlExample = @"https://cf.nascar.com/live/feeds/live-feed.json"
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.WeekendFeed,
                UrlTemplate = @"https://cf.nascar.com/cacher/{0}/{1}/{2}/weekend-feed.json",
                UrlExample = @"https://cf.nascar.com/cacher/2023/1/5274/weekend-feed.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.Season },
                    {1, ApiSourceParameterType.SeriesId },
                    {2, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.LoopData,
                UrlTemplate = @"https://cf.nascar.com/loopstats/prod/{0}/{1}/{2}.json",
                UrlExample = @"https://cf.nascar.com/loopstats/prod/2023/2/5314.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.Season },
                    {1, ApiSourceParameterType.SeriesId },
                    {2, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.PitStops,
                UrlTemplate = @"https://cf.nascar.com/cacher/live/series_{0}/{1}/live-pit-data.json",
                UrlExample = @"https://cf.nascar.com/cacher/live/series_2/5314/live-pit-data.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.SeriesId },
                    {1, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.Points,
                UrlTemplate = @"https://cf.nascar.com/live/feeds/series_{0}/{1}/live_points.json",
                UrlExample = @"https://cf.nascar.com/live/feeds/series_2/5314/live_points.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.SeriesId },
                    {1, ApiSourceParameterType.RaceId },
                }
            });

            apiSources.Add(new ApiSource()
            {
                SourceType = ApiSourceType.Schedules,
                UrlTemplate = @"https://cf.nascar.com/cacher/{0}/race_list_basic.json",
                UrlExample = @"https://cf.nascar.com/cacher/2023/race_list_basic.json",
                UrlParameters = new Dictionary<int, ApiSourceParameterType>()
                {
                    {0, ApiSourceParameterType.Season }
                }
            });

            return apiSources;
        }

        protected virtual string BuildDataFilePath()
        {
            var rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "rNascar23");

            if (!Directory.Exists(rootDirectory))
                Directory.CreateDirectory(rootDirectory);

            var dataDirectory = Path.Combine(rootDirectory, "Data");

            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            return Path.Combine(dataDirectory, $"{DataFileName}");
        }

        #endregion
    }
}
