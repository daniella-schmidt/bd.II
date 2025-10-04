using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string CodeFlight { get; set; } = string.Empty;
        public TypeFlight TypeFlight { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime DateFlight { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public DateTime RealArrival { get; set; }
        public DateTime RealDeparture { get; set; }
        public float Duration { get; set; }
        public List<Reservation> Reservations { get; set; } = [];
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; } = null!;
    }
}
