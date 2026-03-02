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
        Task<bool> ExisteClienteAsync(int id);
        // 🔴 VALIDACIONES DE NEGOCIO
        Task<bool> ExisteIdentificacionAsync(string identificacion);
        Task<bool> ExisteIdentificacionEnOtroClienteAsync(int id, string identificacion);
    }
}