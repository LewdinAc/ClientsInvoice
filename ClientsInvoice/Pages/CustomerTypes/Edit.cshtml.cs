using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientsInvoice.Models;

namespace ClientsInvoice.Pages.CustomerTypes
{
    public class EditModel : PageModel
    {
        private readonly ClientsInvoice.Models.CustomerInvoiceDbContext _context;

        public EditModel(ClientsInvoice.Models.CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CustomerType CustomerType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CustomerTypes == null)
            {
                return NotFound();
            }

            var customertype =  await _context.CustomerTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (customertype == null)
            {
                return NotFound();
            }
            CustomerType = customertype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CustomerType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerTypeExists(CustomerType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerTypeExists(int id)
        {
          return _context.CustomerTypes.Any(e => e.Id == id);
        }
    }
}
