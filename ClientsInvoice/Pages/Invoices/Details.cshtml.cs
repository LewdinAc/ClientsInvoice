using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClientsInvoice.Models;

namespace ClientsInvoice.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly ClientsInvoice.Models.CustomerInvoiceDbContext _context;

        public DetailsModel(ClientsInvoice.Models.CustomerInvoiceDbContext context)
        {
            _context = context;
        }

      public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            else 
            {
                Invoice = invoice;
            }
            return Page();
        }
    }
}
