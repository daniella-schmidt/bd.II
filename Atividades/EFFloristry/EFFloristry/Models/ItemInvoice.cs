using EFFloristry.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ItemInvoice
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int InvoiceId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero")]
    public decimal UnitPrice { get; set; }

    // Propriedade calculada (remover do banco)
    [NotMapped]
    public decimal TotalPrice => Quantity * UnitPrice;

    // Navegação
    [ForeignKey(nameof(InvoiceId))]
    public Invoice? Invoice { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
}