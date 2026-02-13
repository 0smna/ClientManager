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

        
        public IActionResult Index()
        {
            return View();
        }

       
        public async Task<IActionResult> ObtenerClientes()
        {
            var response = await _clienteServicio.ObtenerClientesAsync();
            return Json(new { data = response.Data }); 
        }

        
        [HttpPost]
        public async Task<IActionResult> AgregarCliente(ClienteDto clienteDto)
        {
            var response = await _clienteServicio.AgregarClienteAsync(clienteDto);
            return Json(response);
        }

        
        [HttpPost]
        public async Task<IActionResult> ActualizarCliente(ClienteDto clienteDto)
        {
            var response = await _clienteServicio.ActualizarClienteAsync(clienteDto);
            return Json(response);
        }

       
        [HttpPost]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var response = await _clienteServicio.EliminarClienteAsync(id);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var response = await _clienteServicio.ObtenerClientePorIdAsync(id);
            return Json(response);
        }

    }
}