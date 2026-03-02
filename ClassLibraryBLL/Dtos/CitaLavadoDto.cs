using System;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerBLL.Dtos
{
    public class CitaLavadoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un vehículo.")]
        public int VehiculoId { get; set; }

        // ❌ NO Required
        public string? Placa { get; set; }

        [Required(ErrorMessage = "Debe indicar la fecha de la cita.")]
        public DateTime FechaCita { get; set; }

        public int Estado { get; set; }
    }
}