using System;
using System.Net;
using CiFarm.RestApi.Dtos.Auth; // Import DTOs related to the authentication endpoints.
using CiFarm.Utils; // Utility functions that might be used across the API (like HTTP request helpers).
using Cysharp.Threading.Tasks;

namespace CiFarm.RestApi
{
    // Define the RestApi class, inheriting from Builder<RestApi>.
    // This pattern allows building a more flexible API interaction structure.

    public partial class RestApiClient
    {
        // Base endpoint URL for the authentication services.
        // This string will be used to build full API endpoints.
        private readonly string _baseEndpoint = "auth";

        // Helper method to build the full endpoint URL by appending the specific endpoint to the base URL.
        // Example: GetEndpoint("verify-signature") would return "auth/verify-signature".
        private string GetEndpoint(string endpoint)
        {
            return $"{_baseEndpoint}/{endpoint}";
        }

        // Asynchronous method to verify a signature by sending a POST request to the "verify-signature" endpoint.
        // This method will send the VerifySignatureRequest and return the VerifySignatureResponse.
        public async UniTask<VerifySignatureResponse> VerifyMessageAsync(
            VerifySignatureRequest request // The request data to verify the signature
        )
        {
            var endpoint = GetEndpoint("verify-signature"); // Build the full URL for the "verify-signature" endpoint.

            // Make the POST request to the "verify-signature" endpoint, passing the request object and expecting a response of type VerifySignatureResponse.
            return await PostAsync<VerifySignatureRequest, VerifySignatureResponse>(
                endpoint, // Full URL of the endpoint
                request // Request data (VerifySignatureRequest)
            );
        }

        // Asynchronous method to request a message by sending a POST request to the "request-message" endpoint.
        // This method will send the RequestMessageRequest and return the RequestMessageResponse.
        public async UniTask<RequestMessageResponse> RequestMessageAsync(
            RequestMessageRequest request // The request data to get the message
        )
        {
            var endpoint = GetEndpoint("request-message"); // Build the full URL for the "request-message" endpoint.

            // Make the POST request to the "request-message" endpoint, passing the request object and expecting a response of type RequestMessageResponse.
            return await PostAsync<RequestMessageRequest, RequestMessageResponse>(
                endpoint, // Full URL of the endpoint
                request // Request data (RequestMessageRequest)
            );
        }

        // Asynchronous method to generate a signature by sending a POST request to the "generate-signature" endpoint.
        // This method will send the GenerateSignatureRequest and return the GenerateSignatureResponse.
        public async UniTask<GenerateSignatureResponse> GenerateSignatureAsync(
            GenerateSignatureRequest request // The request data to generate the signature
        )
        {
            var endpoint = GetEndpoint("generate-signature"); // Build the full URL for the "generate-signature" endpoint.

            // Make the POST request to the "generate-signature" endpoint, passing the request object and expecting a response of type GenerateSignatureResponse.
            return await PostAsync<GenerateSignatureRequest, GenerateSignatureResponse>(
                endpoint, // Full URL of the endpoint
                request // Request data (GenerateSignatureRequest)
            );
        }

        // Asynchronous method to refresh the authentication tokens by sending a POST request to the "refresh" endpoint.
        // This method will send the RefreshRequest and return the RefreshResponse.
        public async UniTask<RefreshResponse> RefreshAsync(RefreshRequest request)
        {
            var endpoint = GetEndpoint("refresh"); // Build the full URL for the "refresh" endpoint.

            // Make the POST request to the "refresh" endpoint, passing the request object and expecting a response of type RefreshResponse.
            return await PostAsync<RefreshRequest, RefreshResponse>(endpoint, request);
        }

        //a method to send the post with reva token
        // Send a POST request with error handling
        public async UniTask<TResponse> PostAuthAsync<TRequest, TResponse>(
            string endpoint,
            TRequest requestBody
        )
        {
            var accessToken = AuthToken.GetAccessToken(); // Get the access token from the AuthToken class.
            if (string.IsNullOrEmpty(accessToken)) // Check if the access token is empty or null.
            {
                throw new Exception("Access token not found."); // Throw an exception if the access token is not found.
            }

            try
            {
                return await PostAsync<TRequest, TResponse>( // Make a POST request with the provided request body and return the response.
                    endpoint, // The endpoint URL
                    requestBody, // The request body
                    new() // Additional headers (in this case, the Authorization header with the access token)
                    {
                        { "Authorization", $"Bearer {accessToken}" },
                    }
                );
            }
            catch (RestApiClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var refreshRequest = new RefreshRequest
                    {
                        RefreshToken = AuthToken.GetRefreshToken(),
                    };
                    // Create a new RefreshRequest object with the refresh token.
                    var refreshResponse = await RefreshAsync(refreshRequest); // Call the RefreshAsync method to get a new access token.
                    AuthToken.Save(refreshResponse.AccessToken, refreshRequest.RefreshToken); // Set the new access token in the AuthToken class.

                    return await PostAsync<TRequest, TResponse>( // Make a POST request with the provided request body and return the response.
                        endpoint, // The endpoint URL
                        requestBody, // The request body
                        new() // Additional headers (in this case, the Authorization header with the access token)
                        {
                            { "Authorization", $"Bearer {refreshResponse.AccessToken}" },
                        }
                    );
                }
                throw; // Re-throw the exception if it's not an unauthorized error.
            }
        }
    }
}
