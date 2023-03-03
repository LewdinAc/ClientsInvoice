using ClientsInvoice.Models;
using ClientsInvoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClientsInvoice.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        [BindProperty]
        public CustomerViewModel Customer { get; set; } = default!;

        public EditModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            Customer = new CustomerViewModel();
            Customer.MapProperties(customer);
            ViewData["CostumerTypeId"] = new SelectList(_context.CustomerTypes, "Id", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == Customer.Id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = Customer.Name;
            customer.Address = Customer.Address;
            customer.IsActive = Customer.IsActive;
            customer.CostumerTypeId = Customer.CostumerTypeId;

            _context.Customers.Update(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
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

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
