using EFFloristry.Models;
using EFFloristry.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFFloristry.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private object _logger;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _logger = null;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clientRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _clientRepository.Create(client);
                    TempData["SuccessMessage"] = "Cliente criado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao criar o cliente.");
                }
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = await _clientRepository.GetById(id);
                if (client == null)
                {
                    return NotFound();
                }

                await _clientRepository.Delete(client);
                TempData["SuccessMessage"] = "Cliente excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir o cliente.";
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
                var client = await _clientRepository.GetById(id.Value);
                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // CORREÇÃO: Não buscar o cliente existente, deixar o repositório lidar com isso
                    await _clientRepository.Update(client);
                    TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao atualizar o cliente.");
                }
            }

            return View(client);
        }
    }
}