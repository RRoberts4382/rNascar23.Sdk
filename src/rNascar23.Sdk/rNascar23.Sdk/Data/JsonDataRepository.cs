using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace rNascar23.Sdk.Data
{
    public abstract class JsonDataRepository
    {
        #region consts

        private const string UserAgentTag = "rNascar23";

        #endregion

        #region fields

        protected readonly ILogger<JsonDataRepository> _logger;

        #endregion

        #region ctor

        protected JsonDataRepository(ILogger<JsonDataRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region protected

        protected virtual string Get(string url)
        {
            try
            {
                var client = new RestClient(url);

                var request = new RestRequest(string.Empty, Method.Get);

                request.AddHeader("User-Agent", UserAgentTag);

                var result = client.Execute(request);

                var json = result.Content;

                if (String.IsNullOrEmpty(json))
                    return String.Empty;

                if (json.Contains("<Error>"))
                {
                    HandleXmlError(url, json);

                    return String.Empty;
                }

                return json;
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error in JsonDataRepository.Get. Url: {url}");
            }

            return string.Empty;
        }

        protected virtual async Task<string> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = new RestClient(url);

                var request = new RestRequest(string.Empty, Method.Get);

                request.AddHeader("User-Agent", UserAgentTag);

                var result = await client.ExecuteGetAsync(request, cancellationToken);

                var json = result.Content;

                if (String.IsNullOrEmpty(json))
                    return String.Empty;

                if (json.Contains("<Error>"))
                {
                    HandleXmlError(url, json);

                    return string.Empty;
                }

                return json;
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error in JsonDataRepository.GetAsync. Url: {url}");
            }

            return string.Empty;
        }

        protected virtual void ExceptionHandler(Exception ex, string message = "")
        {
            string errorMessage = String.IsNullOrEmpty(message) ? ex.Message : message;

            _logger.LogError(ex, errorMessage);
        }

        #endregion

        #region private

        private void HandleXmlError(string url, string xml)
        {
            try
            {
                var errorObject = (Error)new XmlSerializer(typeof(Error)).Deserialize(new StringReader(xml));

                _logger.LogInformation($"Error reading data from {url}:\r\nCode: {errorObject.Code}\r\nMessage: {errorObject.Message}\r\nKey: {errorObject.Key}");
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error handling XmlError. Xml: {xml}");
            }
        }

        #endregion
    }
}
