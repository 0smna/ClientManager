using AutoMapper;
using ClientManagerBLL.Dtos;
using ClientManagerDAL.Entities;
using ClientManagerDAL.Repositorios.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBLL.Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _repo;
        private readonly IMapper _mapper;

        public ClienteServicio(IClienteRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync()
        {
            var response = new CustomResponse<List<ClienteDto>>();

            var clientes = await _repo.ObtenerTodosAsync();
            response.Data = _mapper.Map<List<ClienteDto>>(clientes);

            return response;
        }
       

        public async Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(int id)
        {
            var response = new CustomResponse<ClienteDto>();

            if (id == 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id del cliente no puede ser cero.";
                response.codigoStatus = 400;
                return response;
            }

            var cliente = await _repo.ObtenerPorIdAsync(id);

            if (cliente is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente no existe, debe ingresarlo primero.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<ClienteDto>(cliente);
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> AgregarClienteAsync(ClienteDto clienteDto)
        {
            var response = new CustomResponse<ClienteDto>();

            if (clienteDto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El objeto cliente no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _repo.AgregarAsync(cliente);

            response.Data = _mapper.Map<ClienteDto>(cliente);
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> ActualizarClienteAsync(ClienteDto clienteDto)
        {
            var response = new CustomResponse<ClienteDto>();

            if (clienteDto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El objeto cliente no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _repo.ActualizarAsync(cliente);

            response.Data = _mapper.Map<ClienteDto>(cliente);
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> EliminarClienteAsync(int id)
        {
            var response = new CustomResponse<ClienteDto>();

            if (id == 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id del cliente no puede ser cero.";
                response.codigoStatus = 400;
                return response;
            }

            await _repo.EliminarAsync(id);
            return response;
        }
    }
}