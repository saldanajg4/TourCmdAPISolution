using System.Collections.Generic;
using TourCmdAPI.Entities;

namespace TourCmdAPI.Dtos
{
    public class OrderWithItems : Order
    {
        public ICollection<Item> Items { get; set; }
            = new List<Item>();
    }
}