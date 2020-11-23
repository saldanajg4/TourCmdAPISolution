using System.ComponentModel.DataAnnotations;

namespace TourCmdAPI.Dtos
{
    public class IngredientCategoryAbstract
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Category Name is required")]
        [MaxLength(10, ErrorMessage = "maxlength|Name is too long")]
        public string IngredientCategoryName { get; set; }
        
    }
}