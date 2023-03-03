using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClientsInvoice.Models;

namespace ClientsInvoice.Pages.CustomerTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ClientsInvoice.Models.CustomerInvoiceDbContext _context;

        public DeleteModel(ClientsInvoice.Models.CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CustomerType CustomerType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CustomerTypes == null)
            {
                return NotFound();
            }

            var customertype = await _context.CustomerTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (customertype == null)
            {
                return NotFound();
            }
            else 
            {
                CustomerType = customertype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CustomerTypes == null)
            {
                return NotFound();
            }
            var customertype = await _context.CustomerTypes.FindAsync(id);

            if (customertype != null)
            {
                CustomerType = customertype;
                _context.CustomerTypes.Remove(CustomerType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
