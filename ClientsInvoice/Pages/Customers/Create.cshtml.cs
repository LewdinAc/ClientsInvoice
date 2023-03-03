using ClientsInvoice.Models;
using ClientsInvoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientsInvoice.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public CreateModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CostumerTypeId"] = new SelectList(_context.CustomerTypes, "Id", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = new Customer()
            {
                Name = Customer.Name,
                Address = Customer.Address,
                IsActive = Customer.IsActive,
                CostumerTypeId = Customer.CostumerTypeId
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
