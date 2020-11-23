



namespace TourCmdAPI.Entities
{
    public class OrderItem : AuditableEntity
    {
        public int OrderId { get; set; }
     
        public Order Order { get; set; }

        public int ItemId { get; set; }
 
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}