using EFAereoNuvem.Models;
using EFAereoNuvem.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFAereoNuvem.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IScaleRepository _scaleRepository;

        public FlightController(IFlightRepository flightRepository,
                              IAirplaneRepository airplaneRepository,
                              IScaleRepository scaleRepository)
        {
            _flightRepository = flightRepository;
            _airplaneRepository = airplaneRepository;
            _scaleRepository = scaleRepository;
        }

        // ==================== INDEX ====================
        public async Task<IActionResult> Index()
        {
            try
            {
                var flights = await _flightRepository.GetAll();
                return View(flights);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar voos: {ex.Message}";
                return View(new List<Flight>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadAirplanes();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight, List<Scale> scales)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await LoadAirplanes();
                    return View(flight);
                }

                // Validação adicional
                if (flight.AirplaneId == 0)
                {
                    ModelState.AddModelError("AirplaneId", "Selecione uma aeronave.");
                    await LoadAirplanes();
                    return View(flight);
                }

                // Verificar se código do voo já existe
                var existingFlight = await _flightRepository.GetByCode(flight.CodeFlight);
                if (existingFlight != null)
                {
                    ModelState.AddModelError("CodeFlight", "Código do voo já existe.");
                    await LoadAirplanes();
                    return View(flight);
                }

                // Definir horários reais
                flight.RealDeparture = flight.Departure;
                flight.RealArrival = flight.Arrival;

                // Processar escalas
                if (flight.ExistScale && scales != null && scales.Any())
                {
                    flight.Scales = scales.Select((scale, index) => new Scale
                    {
                        Location = scale.Location,
                        Arrival = CombineDateAndTime(flight.DateFlight, scale.Arrival),
                        Departure = CombineDateAndTime(flight.DateFlight, scale.Departure),
                        RealArrival = CombineDateAndTime(flight.DateFlight, scale.Arrival),
                        RealDeparture = CombineDateAndTime(flight.DateFlight, scale.Departure)
                    }).ToList();
                }

                await _flightRepository.Create(flight);
                TempData["SuccessMessage"] = "Voo cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log do erro completo
                Console.WriteLine($"Erro completo: {ex}");
                await LoadAirplanes();
                TempData["ErrorMessage"] = $"Erro ao cadastrar voo: {ex.Message}";
                return View(flight);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var flight = await _flightRepository.GetByIdWithScales(id);
            if (flight == null)
            {
                TempData["ErrorMessage"] = "Voo não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            await LoadAirplanes();
            return View(flight);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Flight flight, List<Scale> scales)
        {
            if (id != flight.Id)
            {
                TempData["ErrorMessage"] = "ID inválido.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                await LoadAirplanes();
                return View(flight);
            }

            try
            {
                // Atualizar escalas se necessário
                if (flight.ExistScale && scales != null && scales.Any())
                {
                    // Remover escalas existentes
                    await _scaleRepository.DeleteByFlightId(flight.Id);

                    // Adicionar novas escalas
                    flight.Scales = scales.Select(scale => new Scale
                    {
                        Location = scale.Location,
                        Arrival = CombineDateAndTime(flight.DateFlight, scale.Arrival),
                        Departure = CombineDateAndTime(flight.DateFlight, scale.Departure),
                        RealArrival = CombineDateAndTime(flight.DateFlight, scale.Arrival),
                        RealDeparture = CombineDateAndTime(flight.DateFlight, scale.Departure),
                        FlightId = flight.Id
                    }).ToList();
                }
                else
                {
                    // Se não possui mais escalas, remover as existentes
                    await _scaleRepository.DeleteByFlightId(flight.Id);
                    flight.Scales = new List<Scale>();
                }

                await _flightRepository.Update(flight);
                TempData["SuccessMessage"] = "Voo atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await LoadAirplanes();
                TempData["ErrorMessage"] = $"Erro ao atualizar voo: {ex.Message}";
                return View(flight);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var flight = await _flightRepository.GetByIdWithScales(id);
            if (flight == null)
            {
                TempData["ErrorMessage"] = "Voo não encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        private async Task LoadAirplanes()
        {
            var airplanes = await _airplaneRepository.GetAll();
            ViewBag.Airplanes = new SelectList(airplanes, "Id", "Prefix");
        }

        private DateTime CombineDateAndTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
        }
    }
}