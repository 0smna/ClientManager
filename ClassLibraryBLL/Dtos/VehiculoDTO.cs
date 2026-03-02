using System.ComponentModel.DataAnnotations;

namespace ClientManagerBLL.Dtos
{
    public class VehiculoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        public int ClienteId { get; set; }
    }
}