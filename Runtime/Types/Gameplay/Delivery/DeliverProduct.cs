namespace CiFarm.Types.Gameplay.Delivery
{
    public class DeliverProductRequest
    {
        public int Index { get; set; }
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
    }

    public class DeliverProductResponse
    {
    }
}
