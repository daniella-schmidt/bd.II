// AuthService.cs
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EFAereoNuvem.Services;
public class AuthService
{
    private readonly HttpClient _httpClient;
    private string? _token;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7157/");
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            var loginModel = new { email, password };
            var json = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/accounts/login", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var responseJson = await response.Content.ReadAsStringAsync();

            // Debug: descomente para ver a resposta
            // Console.WriteLine($"API Response: {responseJson}");

            using var doc = JsonDocument.Parse(responseJson);

            // Acessa a estrutura correta da resposta
            if (doc.RootElement.TryGetProperty("data", out var data) &&
                data.TryGetProperty("token", out var tokenElement))
            {
                _token = tokenElement.GetString();

                if (!string.IsNullOrEmpty(_token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            // Log do erro
            Console.WriteLine($"Erro no AuthService: {ex.Message}");
            return false;
        }
    }

    public void Logout()
    {
        _token = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public bool IsAuthenticated() => !string.IsNullOrEmpty(_token);

    public string? GetToken() => _token;
}