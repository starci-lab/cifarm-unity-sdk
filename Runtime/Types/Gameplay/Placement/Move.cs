using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Placement
{
    public class MoveRequest
    {
        public string PlacedItemTileId { get; set; }
        public Position Position { get; set; }
    }

    public class MoveResponse
    {
    }
}
