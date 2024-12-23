using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Types.Gameplay.Upgrade;
using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Rest
{
    public partial class RestClient
    {
        public async Task<UpgradeBuildingResponse> UpgradeBuilding(UpgradeBuildingRequest request)
        {
            return await PostAsync<UpgradeBuildingRequest, UpgradeBuildingResponse>(BaseConfigs.Endpoints.Gameplay.UpgradeBuilding, request);
        }
    }
}
