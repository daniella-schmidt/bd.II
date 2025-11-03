using System;

namespace EFAereoNuvem.ViewModel
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
        public string ReservationNumber { get; set; }
        public string Status { get; set; } // "Confirmada", "Cancelada", etc.
        public DateTime ReservationDate { get; set; }

        // Informações do Voo
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Airline { get; set; }
        public string AircraftType { get; set; }

        // Informações do Passageiro
        public string PassengerName { get; set; }
        public string PassengerEmail { get; set; }
        public string PassengerPhone { get; set; }

        // Detalhes da Reserva
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
        public string FlightClass { get; set; }
        public string BaggageAllowance { get; set; }
        public string Terminal { get; set; }
        public string Gate { get; set; }
    }
}