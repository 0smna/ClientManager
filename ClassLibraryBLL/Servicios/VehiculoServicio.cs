using AutoMapper;
using ClientManagerBLL.Dtos;
using ClientManagerDAL.Entities;
using ClientManagerDAL.Repositorios.Vehiculos;
using ClientManagerDAL.Repositorios.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBLL.Servicios
{
    public class VehiculoServicio : IVehiculoServicio
    {
        private readonly IVehiculoRepositorio _repo;
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IMapper _mapper;

        public VehiculoServicio(
            IVehiculoRepositorio repo,
            IClienteRepositorio clienteRepo,
            IMapper mapper)
        {
            _repo = repo;
            _clienteRepo = clienteRepo;
            _mapper = mapper;
        }

        // =============================
        // OBTENER TODOS
        // =============================
        public async Task<CustomResponse<List<VehiculoDto>>> ObtenerVehiculosAsync()
        {
            var response = new CustomResponse<List<VehiculoDto>>();

            var vehiculos = await _repo.ObtenerTodosAsync();
            response.Data = _mapper.Map<List<VehiculoDto>>(vehiculos);

            return response;
        }

        // =============================
        // OBTENER POR ID
        // =============================
        public async Task<CustomResponse<VehiculoDto>> ObtenerVehiculoPorIdAsync(int id)
        {
            var response = new CustomResponse<VehiculoDto>();

            if (id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id del vehículo no es válido.";
                return response;
            }

            var vehiculo = await _repo.ObtenerPorIdAsync(id);

            if (vehiculo == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El vehículo no existe.";
                return response;
            }

            response.Data = _mapper.Map<VehiculoDto>(vehiculo);
            return response;
        }

        // =============================
        // AGREGAR
        // =============================
        public async Task<CustomResponse<VehiculoDto>> AgregarVehiculoAsync(VehiculoDto dto)
        {
            var response = new CustomResponse<VehiculoDto>();

            if (dto == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El objeto vehículo no puede ser nulo.";
                return response;
            }

            // 🔥 VALIDAR CLIENTE EXISTE
            var clienteExiste = await _clienteRepo.ExisteClienteAsync(dto.ClienteId);
            if (!clienteExiste)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente seleccionado no existe.";
                return response;
            }

            // 🔥 VALIDAR PLACA DUPLICADA
            var existe = await _repo.ExistePlacaAsync(dto.Placa);
            if (existe)
            {
                response.esCorrecto = false;
                response.mensaje = "Ya existe un vehículo con esa placa.";
                return response;
            }

            var vehiculo = _mapper.Map<Vehiculo>(dto);

            await _repo.AgregarAsync(vehiculo);

            response.Data = _mapper.Map<VehiculoDto>(vehiculo);
            response.mensaje = "Vehículo registrado correctamente.";

            return response;
        }

        // =============================
        // ACTUALIZAR
        // =============================
        public async Task<CustomResponse<VehiculoDto>> ActualizarVehiculoAsync(VehiculoDto dto)
        {
            var response = new CustomResponse<VehiculoDto>();

            if (dto == null || dto.Id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "Datos inválidos para actualizar.";
                return response;
            }

            // 🔥 VALIDAR CLIENTE EXISTE
            var clienteExiste = await _clienteRepo.ExisteClienteAsync(dto.ClienteId);
            if (!clienteExiste)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente seleccionado no existe.";
                return response;
            }

            // 🔥 VALIDAR PLACA EN OTRO VEHÍCULO
            var existe = await _repo.ExistePlacaEnOtroVehiculoAsync(dto.Id, dto.Placa);
            if (existe)
            {
                response.esCorrecto = false;
                response.mensaje = "Ya existe otro vehículo con esa placa.";
                return response;
            }

            var vehiculo = _mapper.Map<Vehiculo>(dto);

            var actualizado = await _repo.ActualizarAsync(vehiculo);

            if (!actualizado)
            {
                response.esCorrecto = false;
                response.mensaje = "No se pudo actualizar el vehículo.";
                return response;
            }

            response.Data = dto;
            response.mensaje = "Vehículo actualizado correctamente.";

            return response;
        }

        // =============================
        // ELIMINAR
        // =============================
        public async Task<CustomResponse<VehiculoDto>> EliminarVehiculoAsync(int id)
        {
            var response = new CustomResponse<VehiculoDto>();

            if (id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id no es válido.";
                return response;
            }

            var eliminado = await _repo.EliminarAsync(id);

            if (!eliminado)
            {
                response.esCorrecto = false;
                response.mensaje = "El vehículo no existe.";
                return response;
            }

            response.mensaje = "Vehículo eliminado correctamente.";
            return response;
        }
    }
}