using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourCmdAPI.Entities
{
    public class PaymentDetail : AuditableEntity
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string CardOwnerName { get; set; }
        [Required]
        [MaxLength(200)]
        public string CardNumber { get; set; }
        [Required]
        public string ExpirationDate { get; set; } 
        [Required]
        [MaxLength(3)]
        public string CVV { get; set; }
    }
}