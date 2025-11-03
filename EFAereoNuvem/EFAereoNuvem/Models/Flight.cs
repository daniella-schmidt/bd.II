using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFAereoNuvem.Models.Enum;

namespace EFAereoNuvem.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Código do voo é obrigatório")]
        [StringLength(10, ErrorMessage = "Código do voo deve ter no máximo 10 caracteres")]
        [Column("CodeFlight", TypeName = "varchar(10)")]
        public string CodeFlight { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo de voo é obrigatório")]
        public TypeFlight TypeFlight { get; set; }

        [Required(ErrorMessage = "Origem é obrigatória")]
        [StringLength(50, ErrorMessage = "Origem deve ter no máximo 50 caracteres")]
        public string Origin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Destino é obrigatório")]
        [StringLength(50, ErrorMessage = "Destino deve ter no máximo 50 caracteres")]
        public string Destination { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data do voo é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime DateFlight { get; set; }

        [Required(ErrorMessage = "Horário de partida é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime Departure { get; set; }

        [Required(ErrorMessage = "Horário de chegada é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime Arrival { get; set; }

        [Required(ErrorMessage = "Chegada real é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime RealArrival { get; set; }

        [Required(ErrorMessage = "Partida real é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime RealDeparture { get; set; }

        public bool ExistScale { get; set; } = false;

        [Required(ErrorMessage = "Duração é obrigatória")]
        [Range(0.1, 24, ErrorMessage = "Duração deve ser entre 0.1 e 24 horas")]
        public float Duration { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Scale> Scales { get; set; } = new List<Scale>();

        [Required(ErrorMessage = "Aeronave é obrigatória")]
        public Guid AirplaneId { get; set; }

        [ForeignKey("AirplaneId")]
        public Airplane Airplane { get; set; }
        public DateTime ArrivalTime { get; internal set; }
        public string? Airline { get; internal set; }
        public DateTime DepartureTime { get; internal set; }
        public string? AircraftType { get; internal set; }
    }
}