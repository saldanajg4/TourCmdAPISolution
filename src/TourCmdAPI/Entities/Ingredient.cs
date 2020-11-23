using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourCmdAPI.Entities
{
    public class Ingredient : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public int IngredientCategoryId { get; set; }
        public IngredientCategory IngredientCategory { get; set; }
        
    }
}