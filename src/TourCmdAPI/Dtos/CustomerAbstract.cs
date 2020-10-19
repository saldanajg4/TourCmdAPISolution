using System.ComponentModel.DataAnnotations;

namespace TourCmdAPI.Dtos
{
    public class CustomerAbstract
    {

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; }
    }
}