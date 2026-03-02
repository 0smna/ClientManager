using ClientManagerBLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBLL.Servicios
{
    public interface ICitaServicio
    {
        Task<CustomResponse<List<CitaLavadoDto>>> ObtenerCitasAsync();
        Task<CustomResponse<CitaLavadoDto>> AgregarCitaAsync(CitaLavadoDto dto);
        Task<CustomResponse<CitaLavadoDto>> CambiarEstadoAsync(int id, int nuevoEstado);
    }
}