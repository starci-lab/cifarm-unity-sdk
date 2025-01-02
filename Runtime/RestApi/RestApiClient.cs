#if CIFARM_SDK_UNITASK_SUPPORT

using CiFarm.Utils;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Net;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using System.Collections.Generic;

namespace CiFarm.RestApi
{
    public enum RestApiVersion
    {
        [EnumStringValue("v1")]
        V1,

        [EnumStringValue("v2")]
        V2,
    }

    public partial class RestApiClient
    {
        // Constructor initializes a new instance of UnityWebRequest
        public RestApiClient() { }

        // Set the base URL
        public string BaseUrl { get; set; }

        // Retry count
        public int RetryCount { get; set; } = 2;

        // 2s for retry interval
        public int RetryInterval { get; set; } = 2000;

        // API version
        public RestApiVersion ApiVersion { get; set; } = RestApiVersion.V1;

        // Send a GET request with error handling
        private async UniTask<TResponse> GetAsync<TResponse>(
            string endpoint,
            int currentRetryCount = 0
        )
        {
            ConsoleLogger.LogDebug($"GET request to '{endpoint}'");
            using var webRequest = new UnityWebRequest
            {
                // Construct the request URL
                url = $"{BaseUrl}/{ApiVersion.GetStringValue()}/{endpoint}",

                // Set the request method to GET
                method = UnityWebRequest.kHttpVerbGET,
            };

            try
            {
                // Send the request
                await webRequest.SendWebRequest();

                // Deserialize the response to TResponse type
                string responseBody = webRequest.downloadHandler.text;

                return JsonConvert.DeserializeObject<TResponse>(responseBody);
            }
            catch (UnityWebRequestException ex)
            {
                if (
                    ex.Result == UnityWebRequest.Result.ConnectionError
                    || ex.Result == UnityWebRequest.Result.ProtocolError
                )
                {
                    if (currentRetryCount < RetryCount)
                    {
                        ConsoleLogger.LogDebug(
                            $"Retrying GET request to '{endpoint}' due to {ex.Result}."
                        );
                        await UniTask.Delay(RetryInterval);
                        return await GetAsync<TResponse>(endpoint, currentRetryCount + 1);
                    }
                    else
                    {
                        throw;
                    }
                }
                throw;
            }
        }

        // Send a POST request with error handling
        public async UniTask<TResponse> PostAsync<TRequest, TResponse>(
            string endpoint,
            TRequest requestBody,
            Dictionary<string, string> additionalHeaders = null,
            int currentRetryCount = 0
        )
            where TRequest : class, new()
            where TResponse : class, new()
        {
            ConsoleLogger.LogDebug($"POST request to '{endpoint}'");
            // JSON serializer setting
            using var webRequest = new UnityWebRequest();

            var jsonBody = JsonConvert.SerializeObject(
                requestBody,
                new EnumAsStringConverter<TRequest>()
            );

            // Construct the request URL
            webRequest.url = $"{BaseUrl}/{ApiVersion.GetStringValue()}/{endpoint}";

            // Set the request method to POST
            webRequest.method = UnityWebRequest.kHttpVerbPOST;

            // Set the request body and content type (assuming you're sending JSON)
            var bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
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
                // Send the request
                await webRequest.SendWebRequest();

                // Deserialize the response to TResponse type
                string responseBody = webRequest.downloadHandler.text;
                return JsonConvert.DeserializeObject<TResponse>(
                    responseBody,
                    new EnumAsStringConverter<TResponse>()
                );
            }
            catch (UnityWebRequestException ex)
            {
                if (
                    ex.Result == UnityWebRequest.Result.ConnectionError
                    || ex.Result == UnityWebRequest.Result.ProtocolError
                )
                {
                    if (currentRetryCount < RetryCount)
                    {
                        // Better retry message with more details
                        ConsoleLogger.LogDebug(
                            $"Retrying POST request to '{endpoint}' due to {ex.Result}."
                        );

                        // Wait for the retry interval before attempting again
                        await UniTask.Delay(RetryInterval);

                        // Recursively call PostAsync for retry
                        return await PostAsync<TRequest, TResponse>(
                            endpoint,
                            requestBody,
                            additionalHeaders,
                            currentRetryCount + 1
                        );
                    }
                    else
                    {
                        // Throw after exceeding retry limit
                        throw;
                    }
                }
                throw;
            }
        }
    }
}

#endif
