using EFAereoNuvem.Models;

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
    public Adress CurrentAdress { get; set; } = null!;
    public int CurrentAdressId { get; set; } // Chave estrangeira para endereço atual

    // Endereço alternativo (opcional)
    public Adress? FutureAdress { get; set; }
    public int? FutureAdressId { get; set; } // Chave estrangeira para endereço futuro (opcional)
}