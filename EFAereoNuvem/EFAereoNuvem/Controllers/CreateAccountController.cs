// CreateAccountController.cs
using EFAereoNuvem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace EFAereoNuvem.Controllers
{
    public class CreateAccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public CreateAccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7157/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ClientCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Prepara o objeto para a API - APENAS email e password
                var registerModel = new
                {
                    email = model.Email,
                    password = model.Password
                };

                var json = JsonSerializer.Serialize(registerModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Enviando para API: {json}"); // Debug

                var response = await _httpClient.PostAsync("v1/accounts/register", content);

                Console.WriteLine($"Status da resposta: {response.StatusCode}"); // Debug

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    // Lê a mensagem de erro da API
                    var errorJson = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro da API: {errorJson}"); // Debug

                    // Tenta extrair a mensagem de erro
                    try
                    {
                        using var errorDoc = JsonDocument.Parse(errorJson);
                        if (errorDoc.RootElement.TryGetProperty("message", out var message))
                        {
                            ModelState.AddModelError(string.Empty, message.GetString() ?? "Erro ao criar conta.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Erro ao criar conta. Tente novamente.");
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Erro ao criar conta. Tente novamente.");
                    }

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção: {ex.Message}"); // Debug
                ModelState.AddModelError(string.Empty, "Erro de conexão. Tente novamente.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}