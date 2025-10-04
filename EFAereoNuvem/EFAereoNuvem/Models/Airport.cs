namespace EFAereoNuvem.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Adress Adress { get; set; } = null!;
    }
}
