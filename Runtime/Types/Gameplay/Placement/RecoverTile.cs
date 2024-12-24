namespace CiFarm.Types.Gameplay.Placement
{
    public class RecoverTileRequest
    {
        public string PlacedItemTileId { get; set; }
    }

    public class RecoverTileResponse
    {
        public string InventoryTileId { get; set; }
    }
}
