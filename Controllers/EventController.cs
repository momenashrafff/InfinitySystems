using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;

namespace InfinitySystems.Controllers
{
    public class EventController : Controller
    {
        private readonly IConfiguration _configuration;

        public EventController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Event()
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            return View();
        }

        [HttpPost]
        [Route("CreateEvent")]
        public IActionResult CreateEvent(Users user)
        {

            return View();
        }

        [HttpPost]
        [Route("AssignEvent")]
        public IActionResult AssignEvent(Users user)
        {

            return View();
        }

        [HttpPost]
        [Route("UninviteEvent")]
        public IActionResult UninviteEvent(Users user)
        {

            return View();
        }

        [HttpPost]
        [Route("SearchEvent")]
        public IActionResult SearchEvent(Users user)
        {

            return View();
        }

        [HttpPost]
        [Route("RemoveEvent")]
        public IActionResult RemoveEvent(Users user)
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