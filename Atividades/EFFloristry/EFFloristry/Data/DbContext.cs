using EFFloristry.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFloristry.Data
{
    public class FloristryContext : DbContext
    {
        public FloristryContext(DbContextOptions<FloristryContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ItemInvoice> ItemInvoices { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento de nomes de tabelas
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<ItemInvoice>().ToTable("Item_Invoice");

            modelBuilder.Entity<ItemInvoice>()
           .HasKey(ii => new { ii.InvoiceId, ii.ProductId });

                // Relacionamento com Invoice
                modelBuilder.Entity<ItemInvoice>()
                    .HasOne(ii => ii.Invoice)
                    .WithMany(i => i.Items)
                    .HasForeignKey(ii => ii.InvoiceId);

                // Relacionamento com Product
                modelBuilder.Entity<ItemInvoice>()
                    .HasOne(ii => ii.Product)
                    .WithMany()
                    .HasForeignKey(ii => ii.ProductId);
        }
    }
}
