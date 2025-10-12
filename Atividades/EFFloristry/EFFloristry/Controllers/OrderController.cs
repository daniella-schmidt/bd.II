using EFFloristry.Models;
using EFFloristry.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFFloristry.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository,
                             IClientRepository clientRepository,
                             IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadViewData();
            return View(new Order { OrderDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orderRepository.Create(order);
                    TempData["SuccessMessage"] = "Pedido criado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao criar o pedido: " + ex.Message);
                }
            }

            await LoadViewData();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);
                if (order == null)
                {
                    return NotFound();
                }

                await _orderRepository.Delete(order);
                TempData["SuccessMessage"] = "Pedido excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir o pedido: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            // CORREÇÃO: Usar o repositório em vez de _context
            var order = await _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            await LoadViewData();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderRepository.Update(order);
                    TempData["SuccessMessage"] = "Pedido atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao atualizar o pedido: " + ex.Message);
                }
            }

            await LoadViewData();
            return View(order);
        }

        [HttpGet]
        public async Task<JsonResult> GetProductPrice(int productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product != null)
            {
                return Json(new { price = product.Price });
            }
            return Json(new { price = 0 });
        }

        private async Task LoadViewData()
        {
            var clients = await _clientRepository.GetAll();
            var products = await _productRepository.GetAll();

            ViewBag.Clients = new SelectList(clients, "ClientId", "Name");
            ViewBag.Products = new SelectList(products, "Id", "ProductDescription");
        }
    }
}