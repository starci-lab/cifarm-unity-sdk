using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Community
{
    public class ThiefAnimalProductRequest : NeighborUserIdRequest
    {
        public string PlacedItemAnimalId { get; set; }
    }

    public class ThiefAnimalProductResponse
    {
        public int Quantity { get; set; }
    }
}
