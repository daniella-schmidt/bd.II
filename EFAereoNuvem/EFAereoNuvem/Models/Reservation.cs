using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guid CodeRersevation { get; set; } = Guid.NewGuid();
        public Class Class { get; set; }
        public float Price { get; set; }
        public DateTime DateReservation { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;
        public int ArmchairId { get; set; }
        public Armchair Armchair { get; set; } = null!;
    }
}
