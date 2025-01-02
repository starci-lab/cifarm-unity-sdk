#if CIFARM_SDK_UNITASK_SUPPORT

using CiFarm.Utils;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Net;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace CiFarm.RestApi
{
    public class RestApiClientException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public UnityWebRequest.Result Result { get; set; }

        public RestApiClientException(
            string message,
            HttpStatusCode statusCode,
            UnityWebRequest.Result result
        )
            : base(message)
        {
            StatusCode = statusCode;
            Result = result;
        }
    }

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
            using var webRequest = new UnityWebRequest
            {
                // Construct the request URL
                url = $"{BaseUrl}/{ApiVersion.GetStringValue()}/{endpoint}",

                // Set the request method to GET
                method = UnityWebRequest.kHttpVerbGET,
            };
            // Send the request
            await webRequest.SendWebRequest();

            // Check if the request was successful
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                //check if currentRetryCount is less than _retryCount
                if (
                    currentRetryCount < RetryCount
                    && webRequest.responseCode != (int)HttpStatusCode.Unauthorized
                    && webRequest.responseCode != (int)HttpStatusCode.Forbidden
                )
                {
                    //wait for _retryInterval before retrying
                    await UniTask.Delay(RetryCount);
                    return await GetAsync<TResponse>(endpoint, currentRetryCount + 1);
                }
                else
                {
                    // Handle errors
                    throw new RestApiClientException(
                        webRequest.error,
                        HttpStatusCode.BadRequest,
                        webRequest.result
                    );
                }
            }

            // Deserialize the response to TResponse type
            string responseBody = webRequest.downloadHandler.text;

            return JsonConvert.DeserializeObject<TResponse>(responseBody);
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
            // JSON serializer setting
            using var webRequest = new UnityWebRequest();

            ConsoleLogger.LogVerbose(JsonConvert.SerializeObject(requestBody));
            var jsonBody = JsonConvert.SerializeObject(
                requestBody,
                new EnumAsStringConverter<TRequest>()
            );
            ConsoleLogger.LogDebug(jsonBody);

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

            // Send the request
            await webRequest.SendWebRequest();

            // Check if the request was successful
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                //check if currentRetryCount is less than _retryCount
                if (
                    currentRetryCount < RetryCount
                    && webRequest.responseCode != (int)HttpStatusCode.Unauthorized
                    && webRequest.responseCode != (int)HttpStatusCode.Forbidden
                )
                {
                    if (webRequest.responseCode == (int)HttpStatusCode.Unauthorized)
                    {
                        // Handle unauthorized errors (e.g., token expired)
                        throw new RestApiClientException(
                            webRequest.error,
                            HttpStatusCode.Unauthorized,
                            webRequest.result
                        );
                    }
                    //wait for _retryInterval before retrying
                    await UniTask.Delay(RetryInterval);
                    return await PostAsync<TRequest, TResponse>(
                        endpoint,
                        requestBody,
                        additionalHeaders,
                        currentRetryCount + 1
                    );
                }
                else
                {
                    // Handle network-related errors (e.g., no connection, server unreachable)
                    throw new RestApiClientException(
                        webRequest.error,
                        HttpStatusCode.BadRequest,
                        webRequest.result
                    );
                }
            }

            // Deserialize the response to TResponse type
            string responseBody = webRequest.downloadHandler.text;
            return JsonConvert.DeserializeObject<TResponse>(
                responseBody,
                new EnumAsStringConverter<TResponse>()
            );
        }
    }
}

#endif
