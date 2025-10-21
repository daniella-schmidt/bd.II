﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAereoNuvem.Models
{
    [Table("Scale")]
    public class Scale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty; // Novo campo

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime RealArrival { get; set; }

        [Required]
        public DateTime RealDeparture { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;
    }
}