using CiFarm.Configs;
using CiFarm.Types.Gameplay.Upgrade;
using System.Threading.Tasks;

namespace CiFarm.Rest
{
    public partial class RestClient
    {
        public async Task<UpgradeBuildingResponse> UpgradeBuilding(UpgradeBuildingRequest request)
        {
            return await PostAsync<UpgradeBuildingRequest, UpgradeBuildingResponse>(BaseConfigs.Endpoints.Gameplay.UpgradeBuilding, request);
        }
    }
}
