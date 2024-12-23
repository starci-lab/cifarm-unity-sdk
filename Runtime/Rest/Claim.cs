using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Types.Gameplay.Claim;
using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Rest
{
    public partial class RestClient
    {
        public async Task<ClaimDailyRewardResponse> ClaimDailyReward(ClaimDailyRewardRequest request)
        {
            return await PostAsync<ClaimDailyRewardRequest, ClaimDailyRewardResponse>(BaseConfigs.Endpoints.Gameplay.ClaimDailyReward, request);
        }

        public async Task<SpinResponse> Spin(SpinRequest request)
        {
            return await PostAsync<SpinRequest, SpinResponse>(BaseConfigs.Endpoints.Gameplay.Spin, request);
        }
    }
}