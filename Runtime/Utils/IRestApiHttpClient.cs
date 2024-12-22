using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Utils
{
    public interface IRestApiHttpClient
    {
        /// <summary>
        /// Configure HttpClient with a base URL and default headers.
        /// </summary>
        /// <param name="baseUrl">Base URL for HttpClient.</param>
        /// <param name="configureHeaders">Action to configure headers.</param>
        void ConfigureClient(string baseUrl, Action<HttpRequestHeaders> configureHeaders);

        /// <summary>
        /// Send a POST request with JSON data to the specified endpoint.
        /// </summary>
        /// <typeparam name="TRequest">Request data type.</typeparam>
        /// <typeparam name="TResponse">Response data type.</typeparam>
        /// <param name="endpoint">API endpoint.</param>
        /// <param name="data">Request data to send.</param>
        /// <returns>Response deserialized into TResponse type.</returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    }
}
