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

         [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int OrderId { get; set; }



    }
}