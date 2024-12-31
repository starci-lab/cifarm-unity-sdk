using CiFarmSDK.RestApi.Dtos;
using CiFarmSDK.Utils;
using Cysharp.Threading.Tasks;

namespace CiFarmSDK.RestApi
{
    public partial class RestApi : Builder<RestApi>
    {
        private readonly string _baseEndpoint = "auth";

        // Get the full endpoint
        private string GetEndpoint(string endpoint)
        {
            return $"{_baseEndpoint}/{endpoint}";
        }

        // Send a POST request to the login endpoint
        public async UniTask<VerifySignatureResponse> VerifyMessageAsync(
            VerifySignatureRequest request
        )
        {
            var endpoint = GetEndpoint("verify-signature");

            return await Post<VerifySignatureRequest, VerifySignatureResponse>(endpoint, request);
        }
    }
}
