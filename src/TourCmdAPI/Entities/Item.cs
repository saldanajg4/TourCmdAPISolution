using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourCmdAPI.Entities
{
    public class Item : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ItemName { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal EstimatedCost { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        //  [ForeignKey("OrderId")]
        // public Order Order { get; set; }

        // public int OrderId { get; set; }



    }
}