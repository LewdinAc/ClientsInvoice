using ClientsInvoice.Models;
using ClientsInvoice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientsInvoice.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly CustomerInvoiceDbContext _context;

        public CreateModel(CustomerInvoiceDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public InvoiceViewModel InvoiceVM { get; set; }

        [BindProperty]
        public InvoiceDetailViewModel InvoiceDetailVM { get; set; }

        [BindProperty]
        public List<InvoiceDetail> InvoiceDetails { get; set; }

        public List<InvoiceDetail> InvoiceDetailList { get; set; }

        public IActionResult OnGet()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.IsActive), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (InvoiceVM.Id == 0)
                {
                    var invoice = new Invoice();
                    invoice.CustomerId = InvoiceVM.CustomerId;
                    invoice.Total = InvoiceVM.Total;
                    invoice.CreatedDate = DateTime.Now;
                    invoice.SubTotal = InvoiceVM.SubTotal;
                }

                var invoiceDetail = new InvoiceDetail
                {
                    Description = InvoiceDetailVM.Description,
                    Quantity = InvoiceDetailVM.Quantity,
                    Price = InvoiceDetailVM.Price,
                    Total = InvoiceDetailVM.Quantity * InvoiceDetailVM.Price
                };

                _context.InvoiceDetails.Add(invoiceDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public void OnPostDetailAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var invoice = new Invoice();
                var invoiceDetail = new InvoiceDetail
                {
                    Description = InvoiceDetailVM.Description,
                    Quantity = InvoiceDetailVM.Quantity,
                    Price = InvoiceDetailVM.Price,
                    Total = InvoiceDetailVM.Quantity * InvoiceDetailVM.Price
                };

                if (InvoiceVM.Id == 0)
                {
                    var itbis = CalculateItbis(invoiceDetail.Total);
                    var total = invoiceDetail.Total + itbis;

                    invoice = new Invoice
                    {
                        CustomerId = InvoiceVM.CustomerId,
                        SubTotal = invoiceDetail.Total,
                        TotalItbis = itbis,
                        Total = total,
                        CreatedDate = DateTime.Now
                    };

                    _context.Invoices.Add(invoice);
                    _context.SaveChanges();

                    invoiceDetail.InvoiceId = invoice.Id;
                    _context.InvoiceDetails.Add(invoiceDetail);
                    _context.SaveChanges();
                }
                else
                {
                    invoice = _context.Invoices.Find(InvoiceVM.Id);

                    if (invoice != null)
                    {
                        var subTotal = invoice.InvoiceDetails.Sum(ind => ind.Total) + invoiceDetail.Total;
                        var itbis = CalculateItbis(subTotal);
                        var total = subTotal + itbis;

                        invoice.CustomerId = InvoiceVM.CustomerId;
                        invoice.SubTotal = invoiceDetail.Total;
                        invoice.TotalItbis = itbis;
                        invoice.Total = total;

                        _context.Invoices.Update(invoice);
                        _context.InvoiceDetails.Add(invoiceDetail);
                        _context.SaveChanges();
                    }
                }


                ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.IsActive), "Id", "Name");
                InvoiceDetails.AddRange(_context.InvoiceDetails.Where(ind => ind.InvoiceId == invoiceDetail.InvoiceId).ToList());
                InvoiceVM.Id = invoice.Id;
                InvoiceVM.CustomerId = invoice.CustomerId;
                InvoiceVM.SubTotal = invoice.SubTotal;
                InvoiceVM.TotalItbis = invoice.TotalItbis;
                InvoiceVM.Total = invoice.Total;

                transaction.Commit();
            }
        }

        private float CalculateItbis(float total)
        {
            return float.Parse((total * 0.18).ToString());
        }
    }
}
