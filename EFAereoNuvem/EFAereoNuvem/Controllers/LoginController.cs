using EFAereoNuvem.Data;
using EFAereoNuvem.Services;
using EFAereoNuvem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFAereoNuvem.Controllers;

public class LoginController : Controller
{
    private readonly AuthService _authService;
    private readonly AppDBContext _context;

    public LoginController(AuthService authService, AppDBContext context)
    {
        _authService = authService;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        Console.WriteLine($"=== TENTATIVA DE LOGIN ===");
        Console.WriteLine($"Email: {model.Email}");

        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState inválido");
            return View(model);
        }

        try
        {
            // Busca o usuário no banco de dados
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                Console.WriteLine("❌ Usuário não encontrado");
                ModelState.AddModelError(string.Empty, "Email ou senha incorretos.");
                return View(model);
            }

            Console.WriteLine($"✅ Usuário encontrado: {user.Email}");

            // Verifica a senha usando PasswordHasher
            var passwordHasher = new PasswordHasher<Models.User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                Console.WriteLine("✅ Senha válida - Login bem-sucedido");

                // REDIRECIONAMENTO CORRETO para ClientDashboard
                TempData["SuccessMessage"] = "Login realizado com sucesso!";
                return RedirectToAction("Index", "ClientDashboard");
            }
            else
            {
                Console.WriteLine("❌ Senha inválida");
                ModelState.AddModelError(string.Empty, "Email ou senha incorretos.");
                return View(model);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro no login: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            ModelState.AddModelError(string.Empty, "Erro interno do sistema. Tente novamente.");
            return View(model);
        }
    }
    [HttpPost]
    public IActionResult Logout()
    {
        _authService.Logout();
        TempData["SuccessMessage"] = "Logout realizado com sucesso!";
        return RedirectToAction("Index");
    }
}