using System.ComponentModel.DataAnnotations;

namespace TourCmdAPI.Dtos
{
    public class EmployeeAbstract
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Name is required")]
        [MaxLength(200, ErrorMessage = "maxlength|Name is too long")]
         public string EmployeeName { get; set; }
    }
}