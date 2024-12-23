using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Types.Gameplay.Auth;
using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Rest
{
    public partial class RestClient
    {
        public async Task<MessageResponse> Message(MessageRequest request)
        {
            return await PostAsync<MessageRequest, MessageResponse>(BaseConfigs.Endpoints.Auth.Message, request);
        }

        public async Task<TestSignatureResponse> TestSignature(TestSignatureRequest request)
        {
            return await PostAsync<TestSignatureRequest, TestSignatureResponse>(BaseConfigs.Endpoints.Auth.TestSignature, request);
        }

        public async Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest request)
        {
            return await PostAsync<VerifySignatureRequest, VerifySignatureResponse>(BaseConfigs.Endpoints.Auth.VerifySignature, request);
        }

        public async Task<RefreshResponse> Refresh(RefreshRequest request)
        {
            return await PostAsync<RefreshRequest, RefreshResponse>(BaseConfigs.Endpoints.Auth.Refresh, request);
        }
    }
}
