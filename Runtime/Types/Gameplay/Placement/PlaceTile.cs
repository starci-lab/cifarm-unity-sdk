using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Placement
{
    public class PlaceTileRequest
    {
        public string InventoryTileId { get; set; }
        public Position Position { get; set; }
    }


    public class PlaceTileResponse
    {
        public string PlacedItemTileId { get; set; }

    }
}
