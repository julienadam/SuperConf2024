using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperConf2024.Models;
using SuperConf2024.Services;
using System.Diagnostics;

namespace SuperConf2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInscriptionService inscription;

        public HomeController(ILogger<HomeController> logger, IInscriptionService inscription)
        {
            _logger = logger;
            this.inscription = inscription;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                PlacesRestantes = inscription.PlacesRestantes()
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
