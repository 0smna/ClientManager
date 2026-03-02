using ClientManagerDAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagerDAL.Repositorios.Vehiculos
{
    public class VehiculoRepositorio : IVehiculoRepositorio
    {
        private static List<Vehiculo> vehiculos = new List<Vehiculo>();

        public async Task<List<Vehiculo>> ObtenerTodosAsync()
        {
            return await Task.FromResult(vehiculos.ToList());
        }

        public async Task<Vehiculo> ObtenerPorIdAsync(int id)
        {
            return await Task.FromResult(
                vehiculos.FirstOrDefault(v => v.Id == id)
            );
        }

        public async Task<bool> AgregarAsync(Vehiculo vehiculo)
        {
            vehiculo.Id = vehiculos.Any() ? vehiculos.Max(v => v.Id) + 1 : 1;
            vehiculos.Add(vehiculo);
            return await Task.FromResult(true);
        }

        public async Task<bool> ActualizarAsync(Vehiculo vehiculo)
        {
            var index = vehiculos.FindIndex(v => v.Id == vehiculo.Id);

            if (index >= 0)
            {
                vehiculos[index] = vehiculo;
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var removed = vehiculos.RemoveAll(v => v.Id == id);
            return await Task.FromResult(removed > 0);
        }

        public async Task<bool> ExistePlacaAsync(string placa)
        {
            return await Task.FromResult(
                vehiculos.Any(v => v.Placa == placa)
            );
        }

        public async Task<bool> ExistePlacaEnOtroVehiculoAsync(int id, string placa)
        {
            return await Task.FromResult(
                vehiculos.Any(v => v.Id != id && v.Placa == placa)
            );
        }
    }
}