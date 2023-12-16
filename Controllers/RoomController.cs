using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InfinitySystems.Controllers
{
    public class RoomController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HomesyncContext _context;

        public RoomController(IConfiguration configuration, HomesyncContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            Room room = new();
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            ViewBag.Rooms = _context.Rooms.FromSqlRaw($"ViewMyRoom {Id.Value}").ToList();
            ViewBag.NotUsedRooms = _context.Rooms.FromSqlRaw($"ViewRoom").ToList();
            ViewBag.isAdmin = HttpContext.Session.GetString("SessionUserType") == "Admin";
            return View(room);
        }

        /*
        Signed-in User
            a) View the assigned room of a user. (Done)
            b) Book a room. (Done)
        
        Signed-in Admin
            a) Create schedule for a room. (Done)
            b) Change status of room. (Done)
            c) View rooms that are not being used. 
        */

        [HttpPost]
        public IActionResult RoomBook(Room room)
        {
            IEnumerable<Room> rooms = _context.Rooms.FromSqlRaw($"SELECT * FROM Room WHERE Id = {room.Id}").ToList();

            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            if (!rooms.IsNullOrEmpty())
            {
                int result = _context.Database.ExecuteSqlInterpolated($"AssignRoom @user_id={Id.Value}, @room_id={room.Id}");
                if (result > 0)
                {
                    TempData["Book"] = "Room Booked Successfully";
                    return RedirectToAction("Index", "Room");
                }

            }
            TempData["Not Book"] = "Room Not Available";
            return RedirectToAction("Index", "Room");
        }

        [HttpPost]
        public IActionResult RoomSchedule(int RoomId, DateTime start, DateTime end, string action)
        {
            IEnumerable<Room> rooms = _context.Rooms.FromSqlRaw($"SELECT * FROM Room WHERE Id = {RoomId}").ToList();
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            if (!rooms.IsNullOrEmpty())
            {
                int res = _context.Database.ExecuteSqlInterpolated($"CreateSchedule @creator_id={Id.Value}, @room_id={RoomId}, @start_time={start}, @end_time={end}, @action={action}");
                if (res > 0)
                {
                    TempData["Schedule"] = "Schedule Created Successfully";
                    return RedirectToAction("Index", "Room");
                }
            }
            TempData["Not Schedule"] = "Schedule Not Created";
            return RedirectToAction("Index", "Room");
        }

        public IActionResult RoomStatus(int RoomId, string status)
        {
            IEnumerable<Room> rooms = _context.Rooms.FromSqlRaw($"SELECT * FROM Room WHERE Id = {RoomId}").ToList();
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            if (!rooms.IsNullOrEmpty())
            {
                int res = _context.Database.ExecuteSqlInterpolated($"RoomAvailability @location={RoomId}, @status={status}");
                if (res > 0)
                {
                    TempData["Status"] = "Room Status Changed Successfully";
                    return RedirectToAction("Index", "Room");
                }
            }
            TempData["Not Status"] = "Room Status Not Changed";
            return RedirectToAction("Index", "Room");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}