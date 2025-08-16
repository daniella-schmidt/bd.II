using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFFloristry.Models
{
    public class ItemInvoice
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }

        public Invoice? Invoice { get; set; }
        public Product? Product { get; set; }
        public ICollection<ItemInvoice>? Items { get; set; }

    }
}
