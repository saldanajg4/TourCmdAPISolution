using System.ComponentModel.DataAnnotations;

namespace TourCmdAPI.Dtos
{
    public class CustomerAbstract
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Name is required.")]
        [MaxLength(200, ErrorMessage = "maxlength|Name is too long.")]
        public string CustomerName { get; set; }
    }
}