using System.Diagnostics;
using System.Runtime.CompilerServices;
using EFTest.Data;
using EFTest.Models;
using EFTest.Repository;
using Microsoft.AspNetCore.Mvc;


namespace EFTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository _studentRepository;
        public HomeController(ILogger<HomeController> logger, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _studentRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentRepository.Create(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        { 
            var student = await _studentRepository.GetById(Id);
            if (student == null)
            {
                return NotFound();
            }
            await _studentRepository.Delete(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // Método POST para processar a atualização
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _studentRepository.Update(student);
                    TempData["SuccessMessage"] = "Estudante atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar estudante {Id}", id);
                    ModelState.AddModelError("", "Erro ao atualizar o estudante.");
                }
            }

            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
