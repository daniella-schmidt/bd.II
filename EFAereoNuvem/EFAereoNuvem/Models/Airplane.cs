﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAereoNuvem.Models;

[Table("Airplanes")]
public class Airplane
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O prefixo é obrigatório.")]
    [StringLength(5, ErrorMessage = "O prefixo deve ter no máximo 5 caracteres.")]
    [Column("Prefix", TypeName = "varchar(5)")]
    public string Prefix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres.")]
    [Column("Type", TypeName = "varchar(50)")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "A capacidade é obrigatória.")]
    [Column("Capacity")]
    public int Capacity { get; set; }

    public List<Flight> Flights { get; set; } = [];
}
