using CiFarm.Configs;
using CiFarm.Types.Gameplay.Shop;
using System.Threading.Tasks;

namespace CiFarm.Rest
{
    public partial class RestClient
    {
        public async Task<BuySeedsResponse> BuySeeds(BuySeedsRequest request)
        {
            return await PostAsync<BuySeedsRequest, BuySeedsResponse>(BaseConfigs.Endpoints.Gameplay.BuySeeds, request);
        }

        public async Task<BuyAnimalResponse> BuyAnimal(BuyAnimalRequest request)
        {
            return await PostAsync<BuyAnimalRequest, BuyAnimalResponse>(BaseConfigs.Endpoints.Gameplay.BuyAnimal, request);
        }

        public async Task<BuyTileResponse> BuyTile(BuyTileRequest request)
        {
            return await PostAsync<BuyTileRequest, BuyTileResponse>(BaseConfigs.Endpoints.Gameplay.BuyTile, request);
        }
    }
}
