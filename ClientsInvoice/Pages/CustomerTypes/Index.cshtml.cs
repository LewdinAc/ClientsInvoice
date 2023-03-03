using ClientsInvoice.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ClientsInvoice.Pages.CustomerTypes
{
    public class IndexModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        public IndexModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public IList<CustomerType> CustomerType { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CustomerTypes != null)
            {
                CustomerType = await _context.CustomerTypes.ToListAsync();
            }
        }
    }
}
