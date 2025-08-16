using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace EFFloristry.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerName { get; set; }
        public string? SellerName { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public float TotalValue { get; set; }
        public ICollection<ItemInvoice>? Items { get; set; }

    }
}