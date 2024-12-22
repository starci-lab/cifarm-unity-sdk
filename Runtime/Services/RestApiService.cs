using StarCi.CiFarmSDK.Configs;
using StarCi.CiFarmSDK.Types.Gameplay.Auth;
using StarCi.CiFarmSDK.Types.Gameplay.Claim;
using StarCi.CiFarmSDK.Types.Gameplay.Community;
using StarCi.CiFarmSDK.Types.Gameplay.Delivery;
using StarCi.CiFarmSDK.Types.Gameplay.Farming;
using StarCi.CiFarmSDK.Types.Gameplay.Placement;
using StarCi.CiFarmSDK.Types.Gameplay.Profile;
using StarCi.CiFarmSDK.Types.Gameplay.Shop;
using StarCi.CiFarmSDK.Types.Gameplay.Upgrade;
using StarCi.CiFarmSDK.Utils;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace StarCi.CiFarmSDK.Services
{
    public class RestApiService
    {
        private readonly IRestApiHttpClient _apiHttpClient;

        public RestApiService(IRestApiHttpClient apiHttpClient)
        {
            _apiHttpClient = apiHttpClient;
        }

        private async Task<TResponse> ExecuteRequest<TRequest, TResponse>(string endpoint, TRequest request)
        {
            try
            {
                Debug.Log($"[RestApiService] Calling {endpoint}...");
                var response = await _apiHttpClient.PostAsync<TRequest, TResponse>(endpoint, request);
                Debug.Log($"[RestApiService] {endpoint} API call successful.");
                return response;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[RestApiService] Error calling {endpoint}: {ex.Message}");
                return default;
            }
        }

        public async Task<MessageResponse> Message(MessageRequest request)
        {
            return await ExecuteRequest<MessageRequest, MessageResponse>(BaseConfigs.Endpoints.Auth.Message, request);
        }

        public async Task<TestSignatureResponse> TestSignature(TestSignatureRequest request)
        {
            return await ExecuteRequest<TestSignatureRequest, TestSignatureResponse>(BaseConfigs.Endpoints.Auth.TestSignature, request);
        }

        public async Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest request)
        {
            return await ExecuteRequest<VerifySignatureRequest, VerifySignatureResponse>(BaseConfigs.Endpoints.Auth.VerifySignature, request);
        }

        public async Task<RefreshResponse> Refresh(RefreshRequest request)
        {
            return await ExecuteRequest<RefreshRequest, RefreshResponse>(BaseConfigs.Endpoints.Auth.Refresh, request);
        }

        public async Task<ClaimDailyRewardResponse> ClaimDailyReward(ClaimDailyRewardRequest request)
        {
            return await ExecuteRequest<ClaimDailyRewardRequest, ClaimDailyRewardResponse>(BaseConfigs.Endpoints.Gameplay.ClaimDailyReward, request);
        }

        public async Task<SpinResponse> Spin(SpinRequest request)
        {
            return await ExecuteRequest<SpinRequest, SpinResponse>(BaseConfigs.Endpoints.Gameplay.Spin, request);
        }

        public async Task<FollowResponse> Follow(FollowRequest request)
        {
            return await ExecuteRequest<FollowRequest, FollowResponse>(BaseConfigs.Endpoints.Gameplay.Follow, request);
        }

        public async Task<UnfollowResponse> Unfollow(UnfollowRequest request)
        {
            return await ExecuteRequest<UnfollowRequest, UnfollowResponse>(BaseConfigs.Endpoints.Gameplay.Unfollow, request);
        }

        public async Task<VisitResponse> Visit(VisitRequest request)
        {
            return await ExecuteRequest<VisitRequest, VisitResponse>(BaseConfigs.Endpoints.Gameplay.Visit, request);
        }

        public async Task<HelpCureAnimalResponse> HelpCureAnimal(HelpCureAnimalRequest request)
        {
            return await ExecuteRequest<HelpCureAnimalRequest, HelpCureAnimalResponse>(BaseConfigs.Endpoints.Gameplay.HelpCureAnimal, request);
        }

        public async Task<HelpUseHerbicideResponse> HelpUseHerbicide(HelpUseHerbicideRequest request)
        {
            return await ExecuteRequest<HelpUseHerbicideRequest, HelpUseHerbicideResponse>(BaseConfigs.Endpoints.Gameplay.HelpUseHerbicide, request);
        }

        public async Task<HelpUsePesticideResponse> HelpUsePesticide(HelpUsePesticideRequest request)
        {
            return await ExecuteRequest<HelpUsePesticideRequest, HelpUsePesticideResponse>(BaseConfigs.Endpoints.Gameplay.HelpUsePesticide, request);
        }

        public async Task<HelpWaterResponse> HelpWater(HelpWaterRequest request)
        {
            return await ExecuteRequest<HelpWaterRequest, HelpWaterResponse>(BaseConfigs.Endpoints.Gameplay.HelpWater, request);
        }

        public async Task<ReturnResponse> Return(ReturnRequest request)
        {
            return await ExecuteRequest<ReturnRequest, ReturnResponse>(BaseConfigs.Endpoints.Gameplay.Return, request);
        }

        public async Task<ThiefAnimalProductResponse> ThiefAnimalProduct(ThiefAnimalProductRequest request)
        {
            return await ExecuteRequest<ThiefAnimalProductRequest, ThiefAnimalProductResponse>(BaseConfigs.Endpoints.Gameplay.ThiefAnimalProduct, request);
        }

        public async Task<ThiefCropResponse> ThiefCrop(ThiefCropRequest request)
        {
            return await ExecuteRequest<ThiefCropRequest, ThiefCropResponse>(BaseConfigs.Endpoints.Gameplay.ThiefCrop, request);
        }

        public async Task<DeliverProductResponse> DeliverProduct(DeliverProductRequest request)
        {
            return await ExecuteRequest<DeliverProductRequest, DeliverProductResponse>(BaseConfigs.Endpoints.Gameplay.DeliverProduct, request);
        }

        public async Task<RetainProductResponse> RetainProduct(RetainProductRequest request)
        {
            return await ExecuteRequest<RetainProductRequest, RetainProductResponse>(BaseConfigs.Endpoints.Gameplay.RetainProduct, request);
        }

        public async Task<CollectAnimalProductResponse> CollectAnimalProduct(CollectAnimalProductRequest request)
        {
            return await ExecuteRequest<CollectAnimalProductRequest, CollectAnimalProductResponse>(BaseConfigs.Endpoints.Gameplay.CollectAnimalProduct, request);
        }

        public async Task<CureAnimalResponse> CureAnimal(CureAnimalRequest request)
        {
            return await ExecuteRequest<CureAnimalRequest, CureAnimalResponse>(BaseConfigs.Endpoints.Gameplay.CureAnimal, request);
        }

        public async Task<FeedAnimalResponse> FeedAnimal(FeedAnimalRequest request)
        {
            return await ExecuteRequest<FeedAnimalRequest, FeedAnimalResponse>(BaseConfigs.Endpoints.Gameplay.FeedAnimal, request);
        }

        public async Task<HarvestCropResponse> HarvestCrop(HarvestCropRequest request)
        {
            return await ExecuteRequest<HarvestCropRequest, HarvestCropResponse>(BaseConfigs.Endpoints.Gameplay.HarvestCrop, request);
        }

        public async Task<PlantSeedResponse> PlantSeed(PlantSeedRequest request)
        {
            return await ExecuteRequest<PlantSeedRequest, PlantSeedResponse>(BaseConfigs.Endpoints.Gameplay.PlantSeed, request);
        }

        public async Task<UseFertilizerResponse> UseFertilizer(UseFertilizerRequest request)
        {
            return await ExecuteRequest<UseFertilizerRequest, UseFertilizerResponse>(BaseConfigs.Endpoints.Gameplay.UseFertilizer, request);
        }

        public async Task<UseHerbicideResponse> UseHerbicide(UseHerbicideRequest request)
        {
            return await ExecuteRequest<UseHerbicideRequest, UseHerbicideResponse>(BaseConfigs.Endpoints.Gameplay.UseHerbicide, request);
        }

        public async Task<UsePesticideResponse> UsePesticide(UsePesticideRequest request)
        {
            return await ExecuteRequest<UsePesticideRequest, UsePesticideResponse>(BaseConfigs.Endpoints.Gameplay.UsePesticide, request);
        }

        public async Task<WaterResponse> Water(WaterRequest request)
        {
            return await ExecuteRequest<WaterRequest, WaterResponse>(BaseConfigs.Endpoints.Gameplay.Water, request);
        }

        public async Task<BuySeedsResponse> BuySeeds(BuySeedsRequest request)
        {
            return await ExecuteRequest<BuySeedsRequest, BuySeedsResponse>(BaseConfigs.Endpoints.Gameplay.BuySeeds, request);
        }

        public async Task<BuyAnimalResponse> BuyAnimal(BuyAnimalRequest request)
        {
            return await ExecuteRequest<BuyAnimalRequest, BuyAnimalResponse>(BaseConfigs.Endpoints.Gameplay.BuyAnimal, request);
        }

        public async Task<BuySuppliesResponse> BuySupplies(BuySuppliesRequest request)
        {
            return await ExecuteRequest<BuySuppliesRequest, BuySuppliesResponse>(BaseConfigs.Endpoints.Gameplay.BuySupplies, request);
        }

        public async Task<BuyTileResponse> BuyTile(BuyTileRequest request)
        {
            return await ExecuteRequest<BuyTileRequest, BuyTileResponse>(BaseConfigs.Endpoints.Gameplay.BuyTile, request);
        }

        public async Task<ConstructBuildingResponse> ConstructBuilding(ConstructBuildingRequest request)
        {
            return await ExecuteRequest<ConstructBuildingRequest, ConstructBuildingResponse>(BaseConfigs.Endpoints.Gameplay.ConstructBuilding, request);
        }

        public async Task<MoveResponse> Move(MoveRequest request)
        {
            return await ExecuteRequest<MoveRequest, MoveResponse>(BaseConfigs.Endpoints.Gameplay.Move, request);
        }

        public async Task<PlaceTileResponse> PlaceTile(PlaceTileRequest request)
        {
            return await ExecuteRequest<PlaceTileRequest, PlaceTileResponse>(BaseConfigs.Endpoints.Gameplay.PlaceTile, request);
        }

        public async Task<RecoverTileResponse> RecoverTile(RecoverTileRequest request)
        {
            return await ExecuteRequest<RecoverTileRequest, RecoverTileResponse>(BaseConfigs.Endpoints.Gameplay.RecoverTile, request);
        }

        public async Task<UpdateTutorialResponse> UpdateTutorial(UpdateTutorialRequest request)
        {
            return await ExecuteRequest<UpdateTutorialRequest, UpdateTutorialResponse>(BaseConfigs.Endpoints.Gameplay.UpdateTutorial, request);
        }

        public async Task<UpgradeBuildingResponse> UpgradeBuilding(UpgradeBuildingRequest request)
        {
            return await ExecuteRequest<UpgradeBuildingRequest, UpgradeBuildingResponse>(BaseConfigs.Endpoints.Gameplay.UpgradeBuilding, request);
        }

    }
}
