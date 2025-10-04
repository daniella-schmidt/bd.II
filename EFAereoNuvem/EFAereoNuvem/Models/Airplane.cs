namespace EFAereoNuvem.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Prefix { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
