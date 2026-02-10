using ClientManagerBLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBLL.Servicios
{
    public interface IClienteServicio
    {
        Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync();
        Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(int id);
        Task<CustomResponse<ClienteDto>> AgregarClienteAsync(ClienteDto clienteDto);
        Task<CustomResponse<ClienteDto>> ActualizarClienteAsync(ClienteDto clienteDto);
        Task<CustomResponse<ClienteDto>> EliminarClienteAsync(int id);
    }
}