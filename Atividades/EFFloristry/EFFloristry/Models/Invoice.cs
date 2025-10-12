using System.ComponentModel.DataAnnotations;

namespace EFFloristry.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }

        [StringLength(200)]
        public string? CustomerAddress { get; set; }

        [StringLength(100)]
        public string? CustomerName { get; set; }

        [StringLength(100)]
        public string? SellerName { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.Now;

        // CORREÇÃO: Mudar de float para decimal
        public decimal TotalValue { get; set; }

        public virtual ICollection<ItemInvoice>? Items { get; set; } = new List<ItemInvoice>();
    }
}