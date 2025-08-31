using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace EFFloristry.Models
{
    public class Product
    {
        [Key]
            public int Id { get; set; }
            public string? ProductDescription { get; set; }
            public decimal Price { get; set; }
            public string? Category { get; set; }
            public int? Stock { get; set; }
    }

}

