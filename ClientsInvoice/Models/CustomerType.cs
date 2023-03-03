using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.Models
{
    public class CustomerType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
