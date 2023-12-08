using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;

namespace InfinitySystems.Controllers
{
    public class FinanceController : Controller
    {
        private readonly IConfiguration _configuration;

        public FinanceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
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