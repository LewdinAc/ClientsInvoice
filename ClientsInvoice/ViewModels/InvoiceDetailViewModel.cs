using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.ViewModels
{
    public class InvoiceDetailViewModel
    {
        public int InvoiceId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float Price { get; set; }
    }
}