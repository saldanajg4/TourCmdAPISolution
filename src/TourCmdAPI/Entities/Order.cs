using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourCmdAPI.Entities
{
    public class Order : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

    
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        // [Required]
        // public DateTimeOffset CreatedAt { get; set; }

         [Required]
        //  [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required]
        public Employee Employee { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

    }
}