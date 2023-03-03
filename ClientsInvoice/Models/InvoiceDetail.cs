using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public float Total { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}