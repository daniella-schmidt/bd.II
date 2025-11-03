using Azure;
using EFAereoNuvem.Models;
using EFAereoNuvem.Repository.Interface;
using EFAereoNuvem.ViewModel;
using EFAereoNuvem.ViewModel.ResponseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Controllers;
public class FlightController : Controller
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IScaleRepository _scaleRepository;

    public FlightController(IFlightRepository flightRepository, IAirplaneRepository airplaneRepository, IScaleRepository scaleRepository)
    {
        _flightRepository = flightRepository;
        _airplaneRepository = airplaneRepository;
        _scaleRepository = scaleRepository;
    }

    // INDEX 
    public async Task<IActionResult> Index()
    {
        try
        {
            var flights = await _flightRepository.GetAllAsync();
            return View(flights); // Agora retorna List<Flight>
        }
        catch
        {
            var response = new ResponseViewModel<List<FlightViewModel>>([ConstantsMessage.ERRO_SERVIDOR]);
            ViewBag.ErrorMessage = response.Messages.FirstOrDefault()?.Message;
            return View(new List<Flight>()); // Retorna List<Flight> vazia
        }
    }

    // CRUD
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        await LoadAirplanes();

        return View(new Flight());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Flight flight, List<Scale> scales)
    {
        if (!ModelState.IsValid || !ValidateFlightTimes(flight))
        {
            await LoadAirplanes();
            return View(flight);
        }

        try
        {
            // Verificar se o AirplaneId é válido
            var airplane = await _airplaneRepository.GetById(flight.AirplaneId);
            if (airplane == null)
            {
                ModelState.AddModelError("AirplaneId", "Aeronave não encontrada.");
                await LoadAirplanes();
                return View(flight);
            }

            // Combinar data do voo com horários de partida/chegada
            flight.Departure = CombineDateAndTime(flight.DateFlight, flight.Departure);
            flight.Arrival = CombineDateAndTime(flight.DateFlight, flight.Arrival);

            // Definir horários reais (inicialmente iguais aos programados)
            flight.RealDeparture = flight.Departure;
            flight.RealArrival = flight.Arrival;

            // Gerar código do voo se estiver vazio
            if (string.IsNullOrEmpty(flight.CodeFlight))
            {
                flight.CodeFlight = GenerateFlightCode(flight);
            }

            // Processar escalas - garantir que FlightId está definido
            if (flight.ExistScale && scales != null && scales.Any())
            {
                flight.Scales = scales.Where(scale => !string.IsNullOrEmpty(scale.Location))
                                    .Select((scale, index) => new Scale
                                    {
                                        Id = Guid.NewGuid(),
                                        Location = scale.Location,
                                        Arrival = CombineDateAndTime(flight.DateFlight, scale.Arrival),
                                        Departure = CombineDateAndTime(flight.DateFlight, scale.Departure),
                                        RealArrival = CombineDateAndTime(flight.DateFlight, scale.Arrival),
                                        RealDeparture = CombineDateAndTime(flight.DateFlight, scale.Departure),
                                        FlightId = flight.Id // Garantir que FlightId está definido
                                    }).ToList();
            }
            else
            {
                flight.ExistScale = false;
                flight.Scales = new List<Scale>();
            }

            // Garantir que o voo está ativo
            flight.IsActive = true;

            // Usar CreateAsync em vez de AddAsync
            await _flightRepository.CreateAsync(flight);

            TempData["SuccessMessage"] = "Voo cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Erro ao cadastrar voo: {ex.Message}");
            await LoadAirplanes();
            return View(flight);
        }
    }

    private bool ValidateFlightTimes(Flight flight)
    {
        if (flight.Departure >= flight.Arrival)
        {
            ModelState.AddModelError("Departure", "A partida deve ser anterior à chegada.");
            return false;
        }

        if (flight.DateFlight.Date < DateTime.Today)
        {
            ModelState.AddModelError("DateFlight", "A data do voo não pode ser no passado.");
            return false;
        }

        return true;
    }

    private string GenerateFlightCode(Flight flight)
    {
        var prefix = flight.Origin.Length >= 3 ? flight.Origin.Substring(0, 3).ToUpper() : flight.Origin.ToUpper();
        var suffix = flight.Destination.Length >= 3 ? flight.Destination.Substring(0, 3).ToUpper() : flight.Destination.ToUpper();
        var random = new Random();
        var number = random.Next(100, 999);

        return $"{prefix}{suffix}{number}";
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var flight = await _flightRepository.GetByIdWithScales(id);
        if (flight == null)
        {
            var response = new ResponseViewModel<Flight?>(data: null, message: ConstantsMessage.NENHUM_VOO_ENCONTRADO);

            TempData["ErrorMessage"] = response.Messages.FirstOrDefault()?.Message;
            return RedirectToAction(nameof(Index));
        }

        await LoadAirplanes();
        return View(flight);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid id, Flight flight, List<Scale> scales)
    {
        if (id != flight.Id)
        {
            var response = new ResponseViewModel<Flight>(ConstantsMessage.VOO_NAO_ENCONTRADO);
            TempData["ErrorMessage"] = response.Messages.FirstOrDefault()?.Message;
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

            await _flightRepository.UpdateAsync(flight);

            var response = new ResponseViewModel<Flight>(flight, ConstantsMessage.VOO_ATUALIZADO_COM_SUCESSO);
            TempData["SuccessMessage"] = response.Messages.FirstOrDefault()?.Message;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            await LoadAirplanes();
            var response = new ResponseViewModel<Flight>(
                new List<MessageResponse>
                {
                    new(TypeMessage.ERRO, 5001, $"Erro ao cadastrar voo: {ex.Message}")
                }
            );

            TempData["ErrorMessage"] = response.Messages.FirstOrDefault()?.Message;
            return View(flight);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var flight = await _flightRepository.GetByIdWithScales(id);
        if (flight == null)
        {
            var response = new ResponseViewModel<Flight?>(data: null, message: ConstantsMessage.NENHUM_VOO_ENCONTRADO);
            TempData["ErrorMessage"] = response.Messages.FirstOrDefault()?.Message;
            return RedirectToAction(nameof(Index));
        }
        return View(flight);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _flightRepository.DeleteAsync(id);

            var response = new ResponseViewModel<Flight>(ConstantsMessage.VOO_DELETADO_COM_SUCESSO);
            TempData["SuccessMessage"] = response.Messages.FirstOrDefault()?.Message;
        }
        catch (Exception ex)
        {
            var response = new ResponseViewModel<Flight>(
            new List<MessageResponse>
            {
                new MessageResponse(TypeMessage.ERRO, 5003, $"Erro ao excluir voo: {ex.Message}")
            }
        );
            TempData["ErrorMessage"] = response.Messages.FirstOrDefault()?.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    // FILTROS E CONSULTAS
    [HttpGet]
    public async Task<IActionResult> SearchByRoute(string origin, string destination)
    {
        var flights = await _flightRepository.GetByRouteAsync(origin, destination);
        return View("Index", flights); // Retorna List<Flight>
    }

    [HttpGet]
    public async Task<IActionResult> SearchByDate(DateTime date)
    {
        var flights = await _flightRepository.GetByDateAsync(date);
        return View("Index", flights); // Retorna List<Flight>
    }

    [HttpGet]
    public async Task<IActionResult> AvailableFlights(string origin, string destination, DateTime date)
    {
        var flights = await _flightRepository.GetAvailableFlightsAsync(origin, destination, date);
        if (!flights.Any())
        {
            var response = new ResponseViewModel<List<Flight>>([ConstantsMessage.NENHUM_VOO_DISPONIVEL]);
            TempData["InfoMessage"] = response.Messages.FirstOrDefault()?.Message;
        }

        return View("Index", flights); // Retorna List<Flight>
    }

    [HttpGet]
    public async Task<IActionResult> DirectFlights() // origin, destination
    {
        var flights = await _flightRepository.GetDirectFlightsAsync();
        return View("Index", flights); // Retorna List<Flight>
    }

    [HttpGet]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> SearchByCode(string code)
    {
        var flight = await _flightRepository.GetByCode(code);
        if (flight == null)
        {
            var response = new ResponseViewModel<Flight?>(data: null, message: ConstantsMessage.NENHUM_VOO_ENCONTRADO);
            TempData["InfoMessage"] = response.Messages.FirstOrDefault()?.Message;
            return RedirectToAction(nameof(Index));
        }
        return View("Details", flight);
    }

    // AUXIIARES
    private async Task LoadAirplanes()
    {
        try
        {
            var airplanes = await _airplaneRepository.GetAll();

            //  Criar SelectList corretamente
            ViewBag.Airplanes = new SelectList(airplanes, "Id", "Prefix");

            // Para debug - ver quantas aeronaves foram carregadas
            ViewBag.AirplanesCount = airplanes?.Count ?? 0;
            Console.WriteLine($"Aeronaves carregadas: {ViewBag.AirplanesCount}");
        }
        catch (Exception ex)
        {
            // Log do erro
            Console.WriteLine($"Erro ao carregar aeronaves: {ex.Message}");
            ViewBag.Airplanes = new SelectList(new List<Airplane>(), "Id", "Prefix");
            ViewBag.AirplanesCount = 0;
        }
    }

    private DateTime CombineDateAndTime(DateTime date, DateTime time)
    {
        return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
    }
}