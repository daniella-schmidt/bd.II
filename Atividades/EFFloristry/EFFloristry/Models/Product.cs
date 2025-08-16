using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace EFFloristry.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? ProductDescription { get; set; }
        public float? Price { get; set; }
    }
}

