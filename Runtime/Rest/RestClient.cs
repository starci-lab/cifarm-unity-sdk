using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StarCi.CiFarmSDK.Rest
{
    //abstraction for native http instance
    public partial class RestClient
    {
        //native client
        private HttpClient _httpClient { get; set; }

        public RestClient(string baseUrl)
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Configure HttpClient with a base URL and default headers.
        /// </summary>
        /// <param name="baseUrl">Base URL for HttpClient.</param>
        /// <param name="configureHeaders">Action to configure headers.</param>
        public void ConfigureHeaders(Action<HttpRequestHeaders> configureHeaders)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            configureHeaders?.Invoke(_httpClient.DefaultRequestHeaders);
        }

        //whenever call the destroy method, the HttpClient despose
        public void Dispose()
        {
            _httpClient.Dispose();
        }

        /// <summary>
        /// Send a POST request with JSON data to the specified endpoint.
        /// </summary>
        /// <typeparam name="TRequest">Request data type.</typeparam>
        /// <typeparam name="TResponse">Response data type.</typeparam>
        /// <param name="endpoint">API endpoint.</param>
        /// <param name="data">Request data to send.</param>
        /// <returns>Response deserialized into TResponse type.</returns>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                Debug.Log($"[RestApiService] Calling {endpoint}...");
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                Debug.Log($"[RestApiService] {endpoint} API call successful.");
                return JsonConvert.DeserializeObject<TResponse>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"[RestApiService] Error calling {endpoint}: {ex.Message}");
                throw default;
            }
        }
    }
}