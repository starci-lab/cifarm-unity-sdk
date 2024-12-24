namespace CiFarm.Configs
{
    public static class BaseConfigs
    {
        public static class BaseUrls
        {
            public readonly static string Rest = "https://api.cifarm.dev.starci.net";
            public readonly static string Graphql = "http://localhost:3006";
        }

        public static class Endpoints
        {
            public static class Auth
            {
                public readonly static string Message = "/auth/message";
                public readonly static string TestSignature = "/auth/test-signature";
                public readonly static string VerifySignature = "/auth/verify-signature";
                public readonly static string Refresh = "/auth/refresh";
            }

            public static class Gameplay
            {
                // Claim
                public readonly static string ClaimDailyReward = "/gameplay/claim-daily-reward";
                public readonly static string Spin = "/gameplay/spin";

                // Community
                public readonly static string Follow = "/gameplay/follow";
                public readonly static string Unfollow = "/gameplay/unfollow";
                public readonly static string Visit = "/gameplay/visit";
                public readonly static string HelpCureAnimal = "/gameplay/help-cure-animal";
                public readonly static string HelpUseHerbicide = "/gameplay/help-use-herbicide";
                public readonly static string HelpUsePesticide = "/gameplay/help-use-pesiticide";
                public readonly static string HelpWater = "/gameplay/help-water";
                public readonly static string Return = "/gameplay/return";
                public readonly static string ThiefAnimalProduct = "/gameplay/thief-animal-product";
                public readonly static string ThiefCrop = "/gameplay/thief-crop";

                // Delivery
                public readonly static string DeliverProduct = "/gameplay/deliver-product";
                public readonly static string RetainProduct = "/gameplay/retain-product";

                // Farming
                public readonly static string CollectAnimalProduct = "/gameplay/collect-animal-product";
                public readonly static string CureAnimal = "/gameplay/cure-animal";
                public readonly static string FeedAnimal = "/gameplay/feed-animal";
                public readonly static string HarvestCrop = "/gameplay/harvest-crop";
                public readonly static string PlantSeed = "/gameplay/plant-seed";
                public readonly static string UseFertilizer = "/gameplay/use-fertilizer";
                public readonly static string UseHerbicide = "/gameplay/use-herbicide";
                public readonly static string UsePesticide = "/gameplay/use-pesticide";
                public readonly static string Water = "/gameplay/water";

                // Shop
                public readonly static string BuySeeds = "/gameplay/buy-seeds";
                public readonly static string BuyAnimal = "/gameplay/buy-animal";
                public readonly static string BuySupplies = "/gameplay/buy-supplies";
                public readonly static string BuyTile = "/gameplay/buy-tile";
                public readonly static string ConstructBuilding = "/gameplay/construct-building";

                // Placement
                public readonly static string Move = "/gameplay/move";
                public readonly static string PlaceTile = "/gameplay/place-tile";
                public readonly static string RecoverTile = "/gameplay/recover-tile";

                // Profile
                public readonly static string UpdateTutorial = "/gameplay/update-tutorial";

                // Upgrade
                public readonly static string UpgradeBuilding = "/gameplay/upgrade-building";
            }
        }
    }
}
