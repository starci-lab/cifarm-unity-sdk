using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Community
{
    public class ThiefCropRequest : NeighborUserIdRequest
    {
        public string PlacedItemTileId { get; set; }
    }

    public class ThiefCropResponse
    {
        public int Quantity { get; set; }
    }
}
