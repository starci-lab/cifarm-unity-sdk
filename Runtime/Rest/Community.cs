using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Types.Gameplay.Community;
using System.Threading.Tasks;

namespace StarCi.CiFarmSDK.Rest
{
    public partial class RestClient
    {
        public async Task<FollowResponse> Follow(FollowRequest request)
        {
            return await PostAsync<FollowRequest, FollowResponse>(BaseConfigs.Endpoints.Gameplay.Follow, request);
        }

        public async Task<UnfollowResponse> Unfollow(UnfollowRequest request)
        {
            return await PostAsync<UnfollowRequest, UnfollowResponse>(BaseConfigs.Endpoints.Gameplay.Unfollow, request);
        }

        public async Task<VisitResponse> Visit(VisitRequest request)
        {
            return await PostAsync<VisitRequest, VisitResponse>(BaseConfigs.Endpoints.Gameplay.Visit, request);
        }

        public async Task<HelpCureAnimalResponse> HelpCureAnimal(HelpCureAnimalRequest request)
        {
            return await PostAsync<HelpCureAnimalRequest, HelpCureAnimalResponse>(BaseConfigs.Endpoints.Gameplay.HelpCureAnimal, request);
        }

        public async Task<HelpWaterResponse> HelpWater(HelpWaterRequest request)
        {
            return await PostAsync<HelpWaterRequest, HelpWaterResponse>(BaseConfigs.Endpoints.Gameplay.HelpWater, request);
        }
    }
}
