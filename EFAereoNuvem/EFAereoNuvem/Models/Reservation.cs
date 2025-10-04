using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string CodeRersevation { get; set; } = null!;
        public Class Class { get; set; }
        public float Price { get; set; }
        public DateTime DateReservation { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        // Conexoes de n-n com dados adicionais
        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;
        public int? SeatId { get; set; }
        public Armchair Armchair { get; set; } = null!;
    }
}
