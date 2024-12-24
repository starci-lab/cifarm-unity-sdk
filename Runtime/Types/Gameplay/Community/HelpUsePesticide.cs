using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Community
{
    public class HelpUsePesticideRequest : NeighborUserIdRequest
    {
        public string PlacedItemTileId { get; set; }
    }

    public class HelpUsePesticideResponse
    {
    }
}
