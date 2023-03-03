using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClientsInvoice.Models;

namespace ClientsInvoice.Pages.CustomerTypes
{
    public class CreateModel : PageModel
    {
        private readonly ClientsInvoice.Models.CustomerInvoiceDbContext _context;

        public CreateModel(ClientsInvoice.Models.CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CustomerType CustomerType { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CustomerTypes.Add(CustomerType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
