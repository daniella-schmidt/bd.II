using EFAereoNuvem.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAereoNuvem.Models;

public class Client
{
    public int Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateOnly BornDate { get; set; } 
    public ClientStatus? ClientStatus { get; set; }

    // Endereço atual (obrigatório)
    public int CurrentAdressId { get; set; } // FK
    public Adress CurrentAdress { get; set; } = null!; // Propriedade de navegação 

    // Endereço alternativo (opcional)
    public int? FutureAdressId { get; set; } 
    public Adress? FutureAdress { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = [];
}