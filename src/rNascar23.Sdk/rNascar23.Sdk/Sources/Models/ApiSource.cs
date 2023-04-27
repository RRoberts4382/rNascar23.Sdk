using System.Collections.Generic;

namespace rNascar23.Sdk.Sources.Models
{
    public class ApiSource
    {
        /// <summary>
        /// Type of data provided by the url
        /// </summary>
        public ApiSourceType SourceType { get; set; }
        /// <summary>
        /// The template to use in building the url
        /// </summary>
        public string UrlTemplate { get; set; }
        /// <summary>
        /// An example of a working url
        /// </summary>
        public string UrlExample { get; set; }
        /// <summary>
        /// List of parameters that the url needs
        /// </summary>
        public IDictionary<int, ApiSourceParameterType> UrlParameters { get; set; } = new Dictionary<int, ApiSourceParameterType>();
    }
}
