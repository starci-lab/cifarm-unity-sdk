using System.Collections.Generic;
using System.Text;
using CiFarm.RestApi;
using CiFarm.Utils;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CiFarm.GraphQL
{
    public partial class GraphQLClient
    {
        // Base URL for GraphQL endpoint
        public string BaseUrl { get; set; }

        // Retry count
        public int RetryCount { get; set; } = 2;

        // Retry interval in milliseconds
        public int RetryInterval { get; set; } = 2000;

        public RestApiClient RestApiClient { get; set; }

        // GraphQL Query method with retry mechanism
        public async UniTask<TResponse> QueryAsync<TVariable, TResponse>(
            string query,
            TVariable variables = null,
            Dictionary<string, string> additionalHeaders = null,
            int currentRetryCount = 0
        )
            where TVariable : class, new()
            where TResponse : class, new()
        {
            ConsoleLogger.LogDebug($"GraphQL Query request to '{BaseUrl}'");

            // Construct the request body for GraphQL query
            var requestBody = new { query, variables };

            // Serialize the requestBody to JSON using Newtonsoft.Json
            var jsonBody = JsonConvert.SerializeObject(requestBody, Formatting.Indented);

            // Create a UnityWebRequest for POST method
            using var webRequest = new UnityWebRequest()
            {
                url = $"{BaseUrl}/graphql",
                method = UnityWebRequest.kHttpVerbPOST,
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonBody)),
                downloadHandler = new DownloadHandlerBuffer(),
            };

            // Set content type to application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Set additional headers if any
            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    webRequest.SetRequestHeader(header.Key, header.Value);
                }
            }

            try
            {
                // Send the request and await its response
                await webRequest.SendWebRequest().ToUniTask();

                string responseBody = webRequest.downloadHandler.text;
                return JsonConvert.DeserializeObject<TResponse>(
                    responseBody,
                    new EnumAsStringConverter<TResponse>()
                );
            }
            catch (UnityWebRequestException ex)
            {
                // Handle any other exceptions, retry if necessary
                Debug.LogError(
                    $"Error during request (Attempt {currentRetryCount + 1}/{RetryCount}): {ex.Message}"
                );

                if (currentRetryCount < RetryCount)
                {
                    // Wait for the retry interval before retrying
                    await UniTask.Delay(RetryInterval);

                    // Retry the request
                    return await QueryAsync<TVariable, TResponse>(
                        query,
                        variables,
                        additionalHeaders,
                        currentRetryCount + 1
                    );
                }
                throw;
            }
        }
    }
}
