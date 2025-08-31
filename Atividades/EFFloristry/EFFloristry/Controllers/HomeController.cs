using EFFloristry.Data;
using EFFloristry.Models;
using EFFloristry.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFFloristry.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository; 
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Adicionado para segurança
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.Create(product);
                    TempData["SuccessMessage"] = "Produto criado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar produto");
                    ModelState.AddModelError("", "Erro ao criar o produto.");
                }
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Adicionado para segurança
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                var product = await _productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _productRepository.Delete(product);
                TempData["SuccessMessage"] = "Produto excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir produto {Id}", id);
                TempData["ErrorMessage"] = "Erro ao excluir o produto.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var product = await _productRepository.GetById(id.Value);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto para edição {Id}", id);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.Update(product);
                    TempData["SuccessMessage"] = "Produto atualizado com sucesso!"; 
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar produto {Id}", id); 
                    ModelState.AddModelError("", "Erro ao atualizar o produto."); 
                }
            }
            return View(product);
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