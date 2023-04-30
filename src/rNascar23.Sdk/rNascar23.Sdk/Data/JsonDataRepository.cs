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
        private const string AccessDeniedErrorCode = "AccessDenied";
        protected const int CircuitBreakerLimit = 2;

        #endregion

        #region fields

        protected readonly ILogger<JsonDataRepository> _logger;
        protected int _errorCount = 0;

        #endregion

        #region properties

        public virtual bool CircuitBreakerTripped
        {
            get
            {
                return _errorCount >= CircuitBreakerLimit;
            }
        }

        #endregion

        #region ctor

        protected JsonDataRepository(ILogger<JsonDataRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region public

        public void ResetCircuitBreaker()
        {
            _errorCount = 0;
        }

        #endregion

        #region protected

        protected virtual string Get(string url)
        {
            string json = String.Empty;

            try
            {
                if (!CircuitBreakerTripped)
                {
                    var client = new RestClient(url);

                    var request = new RestRequest(string.Empty, Method.Get);

                    request.AddHeader("User-Agent", UserAgentTag);

                    var result = client.Execute(request);

                    json = result.Content;

                    if (json.Contains("<Error>"))
                    {
                        HandleXmlError(url, json);

                        json = String.Empty;
                    }

                    return json;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error in JsonDataRepository.Get. Url: {url}", json);
            }

            return json;
        }

        protected virtual async Task<string> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            string json = String.Empty;

            try
            {
                if (!CircuitBreakerTripped)
                {
                    var client = new RestClient(url);

                    var request = new RestRequest(string.Empty, Method.Get);

                    request.AddHeader("User-Agent", UserAgentTag);

                    var result = await client.ExecuteGetAsync(request, cancellationToken);

                    json = result.Content;

                    if (json.Contains("<Error>"))
                    {
                        HandleXmlError(url, json);

                        json = string.Empty;
                    }

                    return json;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error in JsonDataRepository.GetAsync. Url: {url}", json);
            }

            return json;
        }

        protected virtual void ExceptionHandler(Exception ex, string message = "")
        {
            string errorMessage = String.IsNullOrEmpty(message) ? ex.Message : message;

            _logger.LogError(ex, errorMessage);
        }

        protected virtual void ExceptionHandler(Exception ex, string message, string json)
        {
            _logger.LogError(ex, $"{message}\r\n\r\njson: {json}\r\nError Count: {_errorCount}");

            IncrementErrorCount();
        }

        protected virtual void IncrementErrorCount()
        {
            _errorCount += 1;

            if (CircuitBreakerTripped)
                _logger.LogError($"*** Circuit Breaker Tripped in {this.GetType().Name} ***");
        }

        #endregion

        #region private

        private void HandleXmlError(string url, string xml)
        {
            try
            {
                var errorObject = (Error)new XmlSerializer(typeof(Error)).Deserialize(new StringReader(xml));

                if (errorObject.Code == AccessDeniedErrorCode)
                {
                    _logger.LogWarning($"Access Denied: {this.GetType().Name}");
                }
                else
                {
                    _logger.LogWarning($"Error reading data from {url}:\r\nCode: {errorObject.Code}\r\nMessage: {errorObject.Message}\r\nKey: {errorObject.Key}");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error handling XmlError. Xml: {xml}");
            }

            IncrementErrorCount();
        }

        #endregion
    }
}
