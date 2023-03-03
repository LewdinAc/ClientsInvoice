using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(70)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(150)]
        public string Address { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CostumerTypeId { get; set; }

        public virtual CustomerType CostumerType { get; set; }
    }
}
