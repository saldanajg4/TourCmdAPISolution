using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    public class Order : OrderAbstractBase
    {
        public int OrderId { get; set; }
        // public ICollection<OrderItem> OrderItems { get; set; } 
        public ICollection<Dtos.Item> Items { get; set; } 
    }
}