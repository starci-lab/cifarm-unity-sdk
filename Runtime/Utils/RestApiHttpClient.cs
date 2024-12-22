using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace StarCi.CiFarmSDK.Utils
{
    public class RestApiHttpClient : IRestApiHttpClient
    {
        private static readonly Lazy<RestApiHttpClient> _instance = new Lazy<RestApiHttpClient>(() => new RestApiHttpClient());

        private readonly HttpClient _httpClient;

        private RestApiHttpClient()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public static RestApiHttpClient Instance => _instance.Value;

        public void ConfigureClient(string baseUrl, Action<HttpRequestHeaders> configureHeaders)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Clear();

            configureHeaders?.Invoke(_httpClient.DefaultRequestHeaders);
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error making POST request to {endpoint}: {ex.Message}", ex);
            }
        }
    }
}
