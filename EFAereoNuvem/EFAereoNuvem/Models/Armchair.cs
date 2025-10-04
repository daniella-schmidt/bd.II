using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Models
{
    public class Armchair
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public Class Class { get; set; }

        public bool Side { get; set; }
        public bool IsAvaliable { get; set; }
        // Conexoes de n-n com dados adicionais
        public List<Reservation> Reservations { get; set; } = [];
    }
}
