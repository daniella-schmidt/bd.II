using System;
using EFAereoNuvem.Models;

namespace EFAereoNuvem.ViewModel
{
    public class FlightViewModel
    {
        public Guid Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public string Airline { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public TimeSpan Duration => ArrivalTime - DepartureTime;
        public DateTime Departure { get; set; }
        public bool ExistScale { get; set; } = true;

        public static FlightViewModel GetFlightViewModel(Flight f)
        {
            return new FlightViewModel
            {
                Id = f.Id,
                FlightNumber = f.CodeFlight,
                Origin = f.Origin,
                Destination = f.Destination,
                DepartureTime = f.Departure,
                ArrivalTime = f.Arrival,
                ExistScale = f.ExistScale,
                AircraftType = f.Airplane?.Type ?? "Não especificado"
            };
        }
    }
}