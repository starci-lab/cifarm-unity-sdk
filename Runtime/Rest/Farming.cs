using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Types.Gameplay.Farming;
using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Rest
{
    public partial class RestClient
    {
        public async Task<PlantSeedResponse> PlantSeed(PlantSeedRequest request)
        {
            return await PostAsync<PlantSeedRequest, PlantSeedResponse>(BaseConfigs.Endpoints.Gameplay.PlantSeed, request);
        }

        public async Task<HarvestCropResponse> HarvestCrop(HarvestCropRequest request)
        {
            return await PostAsync<HarvestCropRequest, HarvestCropResponse>(BaseConfigs.Endpoints.Gameplay.HarvestCrop, request);
        }

        public async Task<FeedAnimalResponse> FeedAnimal(FeedAnimalRequest request)
        {
            return await PostAsync<FeedAnimalRequest, FeedAnimalResponse>(BaseConfigs.Endpoints.Gameplay.FeedAnimal, request);
        }

        public async Task<UseFertilizerResponse> UseFertilizer(UseFertilizerRequest request)
        {
            return await PostAsync<UseFertilizerRequest, UseFertilizerResponse>(BaseConfigs.Endpoints.Gameplay.UseFertilizer, request);
        }
    }
}
