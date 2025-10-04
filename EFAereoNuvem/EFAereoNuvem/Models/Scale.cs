namespace EFAereoNuvem.Models
{
    public class Scale
    {
        public int Id { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public DateTime RealArrival { get; set; }
        public DateTime RealDeparture { get; set; }
        public int AirportId { get; set; }
        public Airport Airport { get; set; } = null!;

    }
}
