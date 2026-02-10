using ClientManagerBLL.Dtos;
using ClientManagerBLL.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClienteManager.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        // Index sayfası
        public IActionResult Index()
        {
            return View();
        }

        // Tüm müşterileri getir (DataTable uyumlu)
        public async Task<IActionResult> ObtenerClientes()
        {
            var response = await _clienteServicio.ObtenerClientesAsync();
            return Json(new { data = response.Data }); // DataTable için gerekli format
        }

        // Müşteri ekle
        [HttpPost]
        public async Task<IActionResult> AgregarCliente(ClienteDto clienteDto)
        {
            var response = await _clienteServicio.AgregarClienteAsync(clienteDto);
            return Json(response);
        }

        // Müşteri güncelle
        [HttpPost]
        public async Task<IActionResult> ActualizarCliente(ClienteDto clienteDto)
        {
            var response = await _clienteServicio.ActualizarClienteAsync(clienteDto);
            return Json(response);
        }

        // Müşteri sil
        [HttpPost]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var response = await _clienteServicio.EliminarClienteAsync(id);
            return Json(response);
        }
    }
}