namespace TourCmdAPI.Dtos
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string Order { get; set; }
        public string Item { get; set; }
    }
}