using CiFarm.Types.Gameplay.Auth;
using System.Threading.Tasks;

namespace CiFarm.Rest.Interfaces
{
    public partial interface IRestClient
    {
        Task<MessageResponse> Message(MessageRequest request);
        Task<TestSignatureResponse> TestSignature(TestSignatureRequest request);
        Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest request);
        Task<RefreshResponse> Refresh(RefreshRequest request);
    }
}
