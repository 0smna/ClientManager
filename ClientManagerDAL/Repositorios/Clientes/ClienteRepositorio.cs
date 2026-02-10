using ClientManagerDAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagerDAL.Repositorios.Clientes
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private static List<Cliente> clientes = new List<Cliente>
        {
            new Cliente { Id = 1, Nombre = "Juan Perez", Correo = "juan.perez@email.com", Telefono = "8888-1111" },
            new Cliente { Id = 2, Nombre = "Maria Gonzalez", Correo = "maria.gonzalez@email.com", Telefono = "8888-2222" },
            new Cliente { Id = 3, Nombre = "Carlos Ramirez", Correo = "carlos.ramirez@email.com", Telefono = "8888-3333" }
        };

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var copia = clientes.Select(c => new Cliente
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Correo = c.Correo,
                Telefono = c.Telefono,
                FechaRegistro = c.FechaRegistro
            }).ToList();

            return await Task.FromResult(copia);
        }

        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            var c = clientes.FirstOrDefault(x => x.Id == id);
            if (c == null) return null;

            return await Task.FromResult(new Cliente
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Correo = c.Correo,
                Telefono = c.Telefono,
                FechaRegistro = c.FechaRegistro
            });
        }

        public async Task<bool> AgregarAsync(Cliente cliente)
        {
            cliente.Id = clientes.Any() ? clientes.Max(x => x.Id) + 1 : 1;

            clientes.Add(new Cliente
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Correo = cliente.Correo,
                Telefono = cliente.Telefono,
                FechaRegistro = cliente.FechaRegistro
            });

            return await Task.FromResult(true);
        }

        public async Task<bool> ActualizarAsync(Cliente cliente)
        {
            var index = clientes.FindIndex(c => c.Id == cliente.Id);
            if (index >= 0)
            {
                clientes[index].Nombre = cliente.Nombre;
                clientes[index].Correo = cliente.Correo;
                clientes[index].Telefono = cliente.Telefono;
                clientes[index].FechaRegistro = cliente.FechaRegistro;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var removed = clientes.RemoveAll(c => c.Id == id);
            return await Task.FromResult(removed > 0);
        }
    }
}