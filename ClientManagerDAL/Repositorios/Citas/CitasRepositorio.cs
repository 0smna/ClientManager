using ClientManagerDAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagerDAL.Repositorios.Citas
{
    public class CitaRepositorio : ICitaRepositorio
    {
        private static List<CitaLavado> citas = new List<CitaLavado>();

        public async Task<List<CitaLavado>> ObtenerTodosAsync()
        {
            return await Task.FromResult(citas.ToList());
        }

        public async Task<CitaLavado> ObtenerPorIdAsync(int id)
        {
            return await Task.FromResult(
                citas.FirstOrDefault(c => c.Id == id)
            );
        }

        public async Task<bool> AgregarAsync(CitaLavado cita)
        {
            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            return await Task.FromResult(true);
        }

        public async Task<bool> ActualizarAsync(CitaLavado cita)
        {
            var index = citas.FindIndex(c => c.Id == cita.Id);

            if (index >= 0)
            {
                citas[index] = cita;
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}