using ClientsInvoice.Models;
using ClientsInvoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClientsInvoice.Pages.Invoices
{
    public class InvoiceDetailCreateModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        public InvoiceDetailCreateModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public InvoiceDetailViewModel InvoiceDetailVM { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                //if (InvoiceDetailVM.InvoiceId == 0)
                //{
                //    var invoice = new Invoice();
                //    invoice.CustomerId = @Model;
                //    invoice.Total = InvoiceVM.Total;
                //    invoice.CreatedDate = DateTime.Now;
                //    invoice.SubTotal = InvoiceVM.SubTotal;
                //}

                //var invoiceDetail = new InvoiceDetail();
                //invoiceDetail.Quantity = InvoiceDetailVM.Quantity;
                //invoiceDetail.Price = InvoiceDetailVM.Price;
                //invoiceDetail.SubTotal = InvoiceDetailVM.Quantity * InvoiceDetailVM.Price;

                //_context.InvoiceDetails.Add(invoiceDetail);
                //await _context.SaveChangesAsync();
            }

            //return RenderAsyncDelegate()
            return RedirectToPage("./Index");
        }
    }
}
