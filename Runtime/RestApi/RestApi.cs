#if CIFARM_SDK_UNITASK_SUPPORT

using CiFarmSDK.Utils;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Net;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;

namespace CiFarmSDK.RestApi
{
    public class RestApiException : System.Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public UnityWebRequest.Result Result { get; set; }

        public RestApiException(
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

    public partial class RestApi : Builder<RestApi>
    {
        // UnityWebRequest object
        private UnityWebRequest _webRequest;

        // Constructor initializes a new instance of UnityWebRequest
        public RestApi()
        {
            _webRequest = new UnityWebRequest();
        }

        // Set the base URL
        private string _baseUrl;

        public RestApi SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        private int _retryCount = 3;

        public RestApi SetRetryCount(int retryCount)
        {
            _retryCount = retryCount;
            return this;
        }

        // 2s for retry interval
        private int _retryInterval = 2000;

        public RestApi SetRetryInterval(int retryInterval)
        {
            _retryInterval = retryInterval;
            return this;
        }

        // Api version
        private RestApiVersion _apiVersion;

        public RestApi SetApiVersion(RestApiVersion apiVersion)
        {
            _apiVersion = apiVersion;
            return this;
        }

        // Send a GET request with error handling
        private async UniTask<TResponse> Get<TResponse>(string endpoint, int currentRetryCount = 0)
        {
            // Construct the request URL
            _webRequest.url = $"{_baseUrl}/{_apiVersion.GetStringValue()}/{endpoint}";

            // Set the request method to GET
            _webRequest.method = UnityWebRequest.kHttpVerbGET;

            // Send the request
            await _webRequest.SendWebRequest();

            // Check if the request was successful
            if (_webRequest.result != UnityWebRequest.Result.Success)
            {
                //check if currentRetryCount is less than _retryCount
                if (
                    currentRetryCount < _retryCount
                    && _webRequest.responseCode != (int)HttpStatusCode.Unauthorized
                    && _webRequest.responseCode != (int)HttpStatusCode.Forbidden
                )
                {
                    //wait for _retryInterval before retrying
                    await UniTask.Delay(_retryInterval);
                    return await Get<TResponse>(endpoint, currentRetryCount + 1);
                }
                else
                {
                    // Handle errors
                    throw new RestApiException(
                        _webRequest.error,
                        HttpStatusCode.BadRequest,
                        _webRequest.result
                    );
                }
            }

            // Deserialize the response to TResponse type
            string responseBody = _webRequest.downloadHandler.text;

            return JsonConvert.DeserializeObject<TResponse>(responseBody);
        }

        // Send a POST request with error handling
        public async UniTask<TResponse> Post<TRequest, TResponse>(
            string endpoint,
            TRequest requestBody,
            int currentRetryCount = 0
        )
        {
            // Serialize the request body to JSON
            string jsonBody = JsonConvert.SerializeObject(requestBody);

            // Construct the request URL
            _webRequest.url = $"{_baseUrl}/{_apiVersion.GetStringValue()}/{endpoint}";

            // Set the request method to POST
            _webRequest.method = UnityWebRequest.kHttpVerbPOST;

            // Set the request body and content type (assuming you're sending JSON)
            var bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            _webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            _webRequest.downloadHandler = new DownloadHandlerBuffer();
            _webRequest.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            await _webRequest.SendWebRequest();

            // Check if the request was successful
            if (_webRequest.result != UnityWebRequest.Result.Success)
            {
                //check if currentRetryCount is less than _retryCount
                if (
                    currentRetryCount < _retryCount
                    && _webRequest.responseCode != (int)HttpStatusCode.Unauthorized
                    && _webRequest.responseCode != (int)HttpStatusCode.Forbidden
                )
                {
                    if (_webRequest.responseCode == (int)HttpStatusCode.Unauthorized)
                    {
                        // Handle unauthorized errors (e.g., token expired)
                        throw new RestApiException(
                            _webRequest.error,
                            HttpStatusCode.Unauthorized,
                            _webRequest.result
                        );
                    }
                    //wait for _retryInterval before retrying
                    await UniTask.Delay(_retryInterval);
                    return await Post<TRequest, TResponse>(
                        endpoint,
                        requestBody,
                        currentRetryCount + 1
                    );
                }
                else
                {
                    // Handle network-related errors (e.g., no connection, server unreachable)
                    throw new RestApiException(
                        _webRequest.error,
                        HttpStatusCode.BadRequest,
                        _webRequest.result
                    );
                }
            }

            // Deserialize the response to TResponse type
            string responseBody = _webRequest.downloadHandler.text;
            return JsonConvert.DeserializeObject<TResponse>(responseBody);
        }
    }
}

#endif
