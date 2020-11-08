namespace TourCmdAPI.Entities
{
    public class OrderItemForCreation : AuditableEntity
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}