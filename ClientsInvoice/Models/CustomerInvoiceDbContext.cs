using Microsoft.EntityFrameworkCore;

namespace ClientsInvoice.Models
{
    public class CustomerInvoiceDbContext : DbContext
    {
        public CustomerInvoiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CustomerType> CustomerTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.Name, c.IsActive },
                name: "IX_Customer_Name_IsActive");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.CostumerTypeId, c.IsActive },
                name: "IX_Customer_CustomerType_IsActive");

            modelBuilder.Entity<Invoice>()
                .HasIndex(c => new { c.CustomerId, c.CreatedDate },
                name: "IX_Invoice_CustomerId_CreatedDate");

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoiceDetails)
                .WithOne(i => i.Invoice);
        }
    }
}