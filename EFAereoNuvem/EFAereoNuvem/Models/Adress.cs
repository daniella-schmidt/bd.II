public class Adress
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;

    // Navegação de volta para o Client (se necessário)
    public Client? Client { get; set; }
}