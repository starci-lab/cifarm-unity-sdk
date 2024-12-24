using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Community
{
    public class HelpWaterRequest : NeighborUserIdRequest
    {
        public string PlacedItemTileId { get; set; }
    }

    public class HelpWaterResponse
    {
    }
}
