using ClientManagerBLL.Dtos;
using ClientManagerBLL.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteManager.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IVehiculoServicio _servicio;

        public VehiculoController(IVehiculoServicio servicio)
        {
            _servicio = servicio;
        }

        // ============================
        // INDEX
        // ============================
        public IActionResult Index()
        {
            return View();
        }

        // ============================
        // LISTAR (DataTables)
        // ============================
        [HttpGet]
        public async Task<IActionResult> ObtenerVehiculos()
        {
            var response = await _servicio.ObtenerVehiculosAsync();

            return Json(new
            {
                data = response.Data ?? new List<VehiculoDto>()
            });
        }

        // ============================
        // OBTENER POR ID (Editar)
        // ============================
        [HttpGet]
        public async Task<IActionResult> ObtenerVehiculoPorId(int id)
        {
            if (id <= 0)
            {
                return Json(new
                {
                    esCorrecto = false,
                    mensaje = "Id inválido."
                });
            }

            var response = await _servicio.ObtenerVehiculoPorIdAsync(id);
            return Json(response);
        }

        // ============================
        // AGREGAR
        // ============================
        [HttpPost]
        public async Task<IActionResult> AgregarVehiculo(VehiculoDto dto)
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

            var response = await _servicio.AgregarVehiculoAsync(dto);
            return Json(response);
        }

        // ============================
        // ACTUALIZAR
        // ============================
        [HttpPost]
        public async Task<IActionResult> ActualizarVehiculo(VehiculoDto dto)
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

            var response = await _servicio.ActualizarVehiculoAsync(dto);
            return Json(response);
        }

        // ============================
        // ELIMINAR
        // ============================
        [HttpPost]
        public async Task<IActionResult> EliminarVehiculo(int id)
        {
            if (id <= 0)
            {
                return Json(new
                {
                    esCorrecto = false,
                    mensaje = "Id inválido."
                });
            }

            var response = await _servicio.EliminarVehiculoAsync(id);
            return Json(response);
        }
    }
}