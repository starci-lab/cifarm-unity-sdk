using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Community
{
    public class HelpUseHerbicideRequest : NeighborUserIdRequest
    {
        public string PlacedItemTileId { get; set; }
    }

    public class HelpUseHerbicideResponse
    {
    }
}
