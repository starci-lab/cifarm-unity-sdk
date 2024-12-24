using CiFarm.Types.Base;

namespace CiFarm.Types.Gameplay.Community
{
    public class HelpCureAnimalRequest : NeighborUserIdRequest
    {
        public string PlacedItemAnimalId { get; set; }
    }

    public class HelpCureAnimalResponse
    {
    }
}
