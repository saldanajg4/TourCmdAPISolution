using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    public class Item : ItemAbstractBase
    {
        public int ItemId { get; set; }
        //  public ICollection<OrderItem> OrderItems { get; set; }
        public int Quantity { get; set; }
    }
}