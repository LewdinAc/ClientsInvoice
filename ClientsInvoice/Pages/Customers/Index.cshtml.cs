using ClientsInvoice.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClientsInvoice.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        public IndexModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                Customer = await _context.Customers
                .Include(c => c.CostumerType).ToListAsync();
            }
        }
    }
}
