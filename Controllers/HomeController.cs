using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using puma.Models;
using System.Diagnostics;

namespace puma.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [OutputCache(Duration =500)]
        public IActionResult Index()
        {
            ViewBag.msg = DateTime.Now;
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