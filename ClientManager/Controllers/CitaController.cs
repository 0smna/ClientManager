using ClientManagerBLL.Dtos;
using ClientManagerBLL.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteManager.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaServicio _servicio;
        private readonly IVehiculoServicio _vehiculoServicio;

        public CitaController(
            ICitaServicio servicio,
            IVehiculoServicio vehiculoServicio)
        {
            _servicio = servicio;
            _vehiculoServicio = vehiculoServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        // =====================================
        // LISTAR CITAS
        // =====================================
        [HttpGet]
        public async Task<IActionResult> ObtenerCitas()
        {
            var response = await _servicio.ObtenerCitasAsync();

            return Json(new
            {
                data = response.Data ?? new List<CitaLavadoDto>()
            });
        }

        // =====================================
        // LISTAR VEHICULOS (para dropdown)
        // =====================================
        [HttpGet]
        public async Task<IActionResult> ObtenerVehiculos()
        {
            var response = await _vehiculoServicio.ObtenerVehiculosAsync();

            return Json(new
            {
                data = response.Data ?? new List<VehiculoDto>()
            });
        }

        // =====================================
        // AGREGAR CITA
        // =====================================
        [HttpPost]
        public async Task<IActionResult> AgregarCita(CitaLavadoDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return Json(new
                {
                    esCorrecto = false,
                    mensaje = string.Join(" | ", errores)
                });
            }

            var response = await _servicio.AgregarCitaAsync(dto);
            return Json(response);
        }

        // =====================================
        // CAMBIAR ESTADO
        // =====================================
        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, int estado)
        {
            var response = await _servicio.CambiarEstadoAsync(id, estado);
            return Json(response);
        }
    }
}