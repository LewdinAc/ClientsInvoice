using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public float SubTotal { get; set; }

        [Required]
        public float TotalItbis { get; set; }

        [Required]
        public float Total { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; }
    }
}