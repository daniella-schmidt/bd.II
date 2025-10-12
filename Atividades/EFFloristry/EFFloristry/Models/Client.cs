using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFFloristry.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Número de telefone inválido")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public virtual List<Order> Orders { get; set; } = new();
    }
}