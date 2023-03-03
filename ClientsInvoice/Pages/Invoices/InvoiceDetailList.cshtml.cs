using ClientsInvoice.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClientsInvoice.Pages.Invoices
{
    public class InvoiceDetailListModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        public InvoiceDetailListModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public IList<InvoiceDetail> InvoiceDetailList { get; set; } = default!;

        public async Task OnGetAsync(int? invoiceId)
        {
            if (invoiceId != null)
            {
                InvoiceDetailList = await _context.InvoiceDetails
                .Where(ind => ind.InvoiceId == invoiceId)
                .Include(i => i.Invoice).ToListAsync();
            }
        }
    }
}