using EFAereoNuvem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EFAereoNuvem.Controllers
{
    public class ClientDashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Simulação de dados - depois substitua por dados reais do banco
            var availableFlights = new List<FlightViewModel>
            {
                new FlightViewModel
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "AZ123",
                    Origin = "São Paulo (GRU)",
                    Destination = "Rio de Janeiro (GIG)",
                    DepartureTime = DateTime.Now.AddDays(1).AddHours(2),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(3).AddMinutes(30),
                    Price = 450.00m,
                    AvailableSeats = 24,
                    Airline = "AeroNuvem",
                    AircraftType = "Boeing 737"
                },
                new FlightViewModel
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "AZ456",
                    Origin = "Rio de Janeiro (GIG)",
                    Destination = "Brasília (BSB)",
                    DepartureTime = DateTime.Now.AddDays(2).AddHours(8),
                    ArrivalTime = DateTime.Now.AddDays(2).AddHours(10).AddMinutes(15),
                    Price = 620.00m,
                    AvailableSeats = 18,
                    Airline = "AeroNuvem",
                    AircraftType = "Airbus A320"
                },
                new FlightViewModel
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "AZ789",
                    Origin = "São Paulo (GRU)",
                    Destination = "Salvador (SSA)",
                    DepartureTime = DateTime.Now.AddDays(3).AddHours(14),
                    ArrivalTime = DateTime.Now.AddDays(3).AddHours(16).AddMinutes(45),
                    Price = 780.00m,
                    AvailableSeats = 12,
                    Airline = "AeroNuvem",
                    AircraftType = "Boeing 737"
                },
                new FlightViewModel
                {
                    Id = Guid.NewGuid(),
                    FlightNumber = "AZ101",
                    Origin = "Brasília (BSB)",
                    Destination = "Fortaleza (FOR)",
                    DepartureTime = DateTime.Now.AddDays(1).AddHours(18),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(21).AddMinutes(30),
                    Price = 890.00m,
                    AvailableSeats = 8,
                    Airline = "AeroNuvem",
                    AircraftType = "Airbus A321"
                }
            };

            return View(availableFlights);
        }

        [HttpPost]
        public IActionResult ReserveFlight(Guid flightId)
        {
            // Aqui você implementaria a lógica de reserva
            // Por enquanto, vamos apenas redirecionar para uma página de confirmação

            TempData["SuccessMessage"] = $"Reserva para o voo {flightId} realizada com sucesso!";
            return RedirectToAction("ReservationConfirmation");
        }

        [HttpGet]
        public IActionResult ReservationConfirmation()
        {
            return View();
        }

        // NOVOS MÉTODOS PARA GERENCIAR RESERVAS

        [HttpGet]
        public IActionResult MyReservations()
        {
            // Simulação de dados de reservas - substitua por dados reais do banco
            var reservations = new List<ReservationViewModel>
            {
                new ReservationViewModel
                {
                    Id = Guid.NewGuid(),
                    ReservationNumber = "RES001",
                    Status = "Confirmada",
                    ReservationDate = DateTime.Now.AddDays(-2),
                    FlightNumber = "AZ123",
                    Origin = "São Paulo (GRU)",
                    Destination = "Rio de Janeiro (GIG)",
                    DepartureTime = DateTime.Now.AddDays(1).AddHours(2),
                    ArrivalTime = DateTime.Now.AddDays(1).AddHours(3).AddMinutes(30),
                    PassengerName = "João Silva",
                    PassengerEmail = "joao.silva@email.com",
                    PassengerPhone = "(11) 99999-9999",
                    SeatNumber = "15A",
                    Price = 450.00m,
                    Airline = "AeroNuvem",
                    AircraftType = "Boeing 737",
                    FlightClass = "Econômica",
                    BaggageAllowance = "1 bagagem de mão + 1 despachada",
                    Terminal = "2",
                    Gate = "B15"
                },
                new ReservationViewModel
                {
                    Id = Guid.NewGuid(),
                    ReservationNumber = "RES002",
                    Status = "Confirmada",
                    ReservationDate = DateTime.Now.AddDays(-1),
                    FlightNumber = "AZ456",
                    Origin = "Rio de Janeiro (GIG)",
                    Destination = "Brasília (BSB)",
                    DepartureTime = DateTime.Now.AddDays(2).AddHours(8),
                    ArrivalTime = DateTime.Now.AddDays(2).AddHours(10).AddMinutes(15),
                    PassengerName = "João Silva",
                    PassengerEmail = "joao.silva@email.com",
                    PassengerPhone = "(11) 99999-9999",
                    SeatNumber = "22C",
                    Price = 620.00m,
                    Airline = "AeroNuvem",
                    AircraftType = "Airbus A320",
                    FlightClass = "Econômica",
                    BaggageAllowance = "1 bagagem de mão + 1 despachada",
                    Terminal = "1",
                    Gate = "A08"
                }
            };

            return View(reservations);
        }

        [HttpGet]
        public IActionResult ReservationDetails(Guid id)
        {
            // Simulação de busca de reserva por ID - substitua por busca real no banco
            var reservation = new ReservationViewModel
            {
                Id = id,
                ReservationNumber = "RES001",
                Status = "Confirmada",
                ReservationDate = DateTime.Now.AddDays(-2),
                FlightNumber = "AZ123",
                Origin = "São Paulo (GRU)",
                Destination = "Rio de Janeiro (GIG)",
                DepartureTime = DateTime.Now.AddDays(1).AddHours(2),
                ArrivalTime = DateTime.Now.AddDays(1).AddHours(3).AddMinutes(30),
                PassengerName = "João Silva",
                PassengerEmail = "joao.silva@email.com",
                PassengerPhone = "(11) 99999-9999",
                SeatNumber = "15A",
                Price = 450.00m,
                Airline = "AeroNuvem",
                AircraftType = "Boeing 737",
                FlightClass = "Econômica",
                BaggageAllowance = "1 bagagem de mão + 1 despachada",
                Terminal = "2",
                Gate = "B15"
            };

            return View(reservation);
        }

        [HttpPost]
        public IActionResult CancelReservation(Guid reservationId)
        {
            // Aqui você implementaria a lógica de cancelamento
            // Por enquanto, vamos apenas redirecionar com mensagem de sucesso

            TempData["SuccessMessage"] = "Reserva cancelada com sucesso!";
            return RedirectToAction("MyReservations");
        }
    }
}