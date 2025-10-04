using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Models
{
    public class ClientStatus
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public bool Priority { get; set; } = false;
        public float Discount { get; set; } = 0;
    }
}
