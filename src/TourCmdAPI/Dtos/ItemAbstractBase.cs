using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    public class ItemAbstractBase
    {
        public string ItemName { get; set; }
        

        public string Description { get; set; }

        public decimal Price { get; set; }
        // public ICollection<Ingredient> Ingredients { get; set; }
        //     = new List<Ingredient>();
    }
}