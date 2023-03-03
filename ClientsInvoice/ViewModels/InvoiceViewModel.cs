using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public float TotalItbis { get; set; }

        [Required]
        public float SubTotal { get; set; }

        [Required]
        public float Total { get; set; }
    }
}