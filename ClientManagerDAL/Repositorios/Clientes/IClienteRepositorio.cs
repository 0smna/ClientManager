using ClientManagerDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerDAL.Repositorios.Clientes
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int id);
      

    }
}