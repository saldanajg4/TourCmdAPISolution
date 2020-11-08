using System;
using System.ComponentModel.DataAnnotations;

namespace TourCmdAPI.Dtos
{
    public class OrderAbstractBase
    {
         [Required(AllowEmptyStrings = false, ErrorMessage = "required|Customer is required.")]
        [MaxLength(200, ErrorMessage = "maxLength|Customer is too long.")]
        public string CustomerName { get; set; }

        [MaxLength(2000, ErrorMessage = "maxLength|Description is too long.")]
        public virtual string Description { get; set; }


    }
}