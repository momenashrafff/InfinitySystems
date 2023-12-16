using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InfinitySystems.Controllers
{
    public class EventController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HomesyncContext _context;

        public EventController(IConfiguration configuration, HomesyncContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            Calendar calendar = new();
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            ViewBag.events = _context.Calendars.FromSqlRaw($"ViewMyRoom {Id.Value}").ToList();
            ViewBag.isAdmin = HttpContext.Session.GetString("SessionUserType") == "Admin";
            return View(calendar);
        }

        [HttpPost]
        [Route("CreateEvent")]
        public IActionResult CreateEvent(Calendar calendar)
        {

            return View();
        }

        [HttpPost]
        [Route("AssignEvent")]
        public IActionResult AssignEvent(Calendar calendar)
        {

            return View();
        }

        [HttpPost]
        [Route("UninviteEvent")]
        public IActionResult UninviteEvent(Calendar calendar)
        {

            return View();
        }

        [HttpPost]
        [Route("SearchEvent")]
        public IActionResult SearchEvent(Calendar calendar)
        {

            return View();
        }

        [HttpPost]
        [Route("RemoveEvent")]
        public IActionResult RemoveEvent(Calendar calendar)
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