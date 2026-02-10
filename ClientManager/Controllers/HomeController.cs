using ClientManagerBLL.Dtos;
using ClientManagerBLL.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClientManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteServicio _servicio;

        public HomeController(IClienteServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _servicio.ObtenerClientesAsync();

            
            List<ClienteDto> lista = response.Data ?? new List<ClienteDto>();

            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(); 
        }
    }
}