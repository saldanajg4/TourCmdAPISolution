namespace TourCmdAPI.Dtos
{
    public class OrderItemForCreation
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}