using ClientManagerDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerDAL.Repositorios.Vehiculos
{
    public interface IVehiculoRepositorio
    {
        // LISTAR TODOS
        Task<List<Vehiculo>> ObtenerTodosAsync();

        // OBTENER POR ID
        Task<Vehiculo> ObtenerPorIdAsync(int id);

        // AGREGAR
        Task<bool> AgregarAsync(Vehiculo vehiculo);

        // ACTUALIZAR
        Task<bool> ActualizarAsync(Vehiculo vehiculo);

        // ELIMINAR
        Task<bool> EliminarAsync(int id);

        // VALIDACIONES DE NEGOCIO
        Task<bool> ExistePlacaAsync(string placa);

        Task<bool> ExistePlacaEnOtroVehiculoAsync(int id, string placa);
    }
}