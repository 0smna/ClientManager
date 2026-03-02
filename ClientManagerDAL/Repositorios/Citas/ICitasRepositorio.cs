using ClientManagerDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerDAL.Repositorios.Citas
{
    public interface ICitaRepositorio
    {
        Task<List<CitaLavado>> ObtenerTodosAsync();
        Task<CitaLavado> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(CitaLavado cita);
        Task<bool> ActualizarAsync(CitaLavado cita);
    }
}