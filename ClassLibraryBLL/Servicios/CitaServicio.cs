using AutoMapper;
using ClientManagerBLL.Dtos;
using ClientManagerDAL.Entities;
using ClientManagerDAL.Repositorios.Citas;
using ClientManagerDAL.Repositorios.Vehiculos;
using ClientManagerDAL.Repositorios.Clientes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ClientManagerBLL.Servicios
{
    public class CitaServicio : ICitaServicio
    {
        private readonly ICitaRepositorio _repo;
        private readonly IVehiculoRepositorio _vehiculoRepo;
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IMapper _mapper;

        public CitaServicio(
            ICitaRepositorio repo,
            IVehiculoRepositorio vehiculoRepo,
            IClienteRepositorio clienteRepo,
            IMapper mapper)
        {
            _repo = repo;
            _vehiculoRepo = vehiculoRepo;
            _clienteRepo = clienteRepo;
            _mapper = mapper;
        }

        // =====================================
        // OBTENER TODAS LAS CITAS
        // =====================================
        public async Task<CustomResponse<List<CitaLavadoDto>>> ObtenerCitasAsync()
        {
            var response = new CustomResponse<List<CitaLavadoDto>>();

            var citas = await _repo.ObtenerTodosAsync();
            var vehiculos = await _vehiculoRepo.ObtenerTodosAsync();

            var lista = citas.Select(c =>
            {
                var vehiculo = vehiculos.FirstOrDefault(v => v.Id == c.VehiculoId);

                return new CitaLavadoDto
                {
                    Id = c.Id,
                    ClienteId = c.ClienteId,
                    VehiculoId = c.VehiculoId,
                    Placa = vehiculo?.Placa,
                    FechaCita = c.FechaCita,
                    Estado = (int)c.Estado
                };
            }).ToList();

            response.Data = lista;
            return response;
        }

        // =====================================
        // AGREGAR CITA
        // =====================================
        public async Task<CustomResponse<CitaLavadoDto>> AgregarCitaAsync(CitaLavadoDto dto)
        {
            var response = new CustomResponse<CitaLavadoDto>();

            if (dto == null)
            {
                response.esCorrecto = false;
                response.mensaje = "Los datos de la cita son inválidos.";
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

            // 🔥 VALIDAR VEHÍCULO EXISTE
            var vehiculo = await _vehiculoRepo.ObtenerPorIdAsync(dto.VehiculoId);
            if (vehiculo == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El vehículo seleccionado no existe.";
                return response;
            }

            // 🔥 VALIDAR VEHÍCULO PERTENECE AL CLIENTE
            if (vehiculo.ClienteId != dto.ClienteId)
            {
                response.esCorrecto = false;
                response.mensaje = "El vehículo no pertenece al cliente seleccionado.";
                return response;
            }

            // 🔥 VALIDAR FECHA NO PASADA
            if (dto.FechaCita.Date < DateTime.Today)
            {
                response.esCorrecto = false;
                response.mensaje = "No se pueden registrar citas en fechas pasadas.";
                return response;
            }

            // 🔥 VALIDAR NO DUPLICAR CITA MISMO VEHÍCULO MISMA FECHA
            var citasExistentes = await _repo.ObtenerTodosAsync();
            var existeDuplicada = citasExistentes.Any(c =>
                c.VehiculoId == dto.VehiculoId &&
                c.FechaCita.Date == dto.FechaCita.Date &&
                c.Estado != EstadoCita.Cancelada);

            if (existeDuplicada)
            {
                response.esCorrecto = false;
                response.mensaje = "Ya existe una cita para ese vehículo en esa fecha.";
                return response;
            }

            var cita = new CitaLavado
            {
                ClienteId = dto.ClienteId,
                VehiculoId = dto.VehiculoId,
                FechaCita = dto.FechaCita,
                Estado = EstadoCita.Ingresada
            };

            await _repo.AgregarAsync(cita);

            response.Data = _mapper.Map<CitaLavadoDto>(cita);
            response.mensaje = "Cita registrada correctamente.";

            return response;
        }

        // =====================================
        // CAMBIAR ESTADO
        // =====================================
        public async Task<CustomResponse<CitaLavadoDto>> CambiarEstadoAsync(int id, int nuevoEstado)
        {
            var response = new CustomResponse<CitaLavadoDto>();

            if (id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id de la cita no es válido.";
                return response;
            }

            var cita = await _repo.ObtenerPorIdAsync(id);

            if (cita == null)
            {
                response.esCorrecto = false;
                response.mensaje = "La cita no existe.";
                return response;
            }

            if (!Enum.IsDefined(typeof(EstadoCita), nuevoEstado))
            {
                response.esCorrecto = false;
                response.mensaje = "Estado inválido.";
                return response;
            }

            cita.Estado = (EstadoCita)nuevoEstado;

            await _repo.ActualizarAsync(cita);

            response.mensaje = "Estado actualizado correctamente.";
            return response;
        }
    }
}