using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Shop
{
    public class BuyAnimalRequest
    {
        public string AnimalId { get; set; }
        public string PlacedItemBuildingId { get; set; }
        public Position Position { get; set; }
    }

    public class BuyAnimalResponse
    {
    }
}
