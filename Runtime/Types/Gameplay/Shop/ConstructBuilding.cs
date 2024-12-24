using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Shop
{
    public class ConstructBuildingRequest
    {
        public string BuildingId { get; set; }
        public Position Position { get; set; }
    }

    public class ConstructBuildingResponse
    {
    }
}
