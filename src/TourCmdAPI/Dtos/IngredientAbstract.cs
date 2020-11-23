using System.ComponentModel.DataAnnotations;

namespace TourCmdAPI.Dtos
{
    public class IngredientAbstract
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Ingredient Name is required")]
        [MaxLength(200, ErrorMessage = "maxlength|Ingredient Name is too long.")]
         public string IngredientName { get; set; }
         public int IngredientCategoryId { get; set; }
        
    }
}