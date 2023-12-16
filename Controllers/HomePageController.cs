using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace InfinitySystems.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ILogger<HomePageController> _logger;
        private readonly HomesyncContext _context;
        private readonly IConfiguration _configuration;

        public HomePageController(ILogger<HomePageController> logger, HomesyncContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            ViewBag.user = _context.User.FromSql($"SELECT * FROM Users WHERE Id = {Id.Value}").ToList();
            ViewBag.guests = (from guest in _context.Guests
                              join admin in _context.Admins on guest.Guest_Of equals admin.Admin_Id
                              where admin.Admin_Id == Id.Value
                              select guest.Guest_Id).Count();
            ViewBag.isAdmin = HttpContext.Session.GetString("SessionUserType") == "Admin";
            return View();
        }

        [HttpPost]
        public IActionResult GuestRemove(int id)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            int? No_Of_Allowed_Guests = _context.Admins.FromSql($"SELECT * FROM Admin WHERE admin_id = {Id.Value}").ToList()[0].No_Of_Guests_Allowed;

            string connection = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand guestRemove = new SqlCommand("GuestRemove", sqlConnection);
            guestRemove.CommandType = System.Data.CommandType.StoredProcedure;
            guestRemove.Parameters.Add(new SqlParameter("@guest_id", id));
            guestRemove.Parameters.Add(new SqlParameter("@admin_id", Id.Value));

            SqlParameter numOfGuests = guestRemove.Parameters.Add(@"number_of_allowed_guests", SqlDbType.Int);
            numOfGuests.Direction = ParameterDirection.Output;
            sqlConnection.Open();
            guestRemove.ExecuteNonQuery();
            sqlConnection.Close();
            if (numOfGuests.Value.ToString().Equals(No_Of_Allowed_Guests.Value.ToString()))
            {
                TempData["Remove"] = "Fail";
            }
            else
            {
                TempData["Remove"] = "Success";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetAllowedGuests(int id, int num)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }

            bool isAdmin = _context.Admins.FromSqlRaw($"SELECT * FROM Admin WHERE Admin.admin_id = {Id}").ToList().IsNullOrEmpty() ? false : true;
            if (isAdmin)
            {
                int result = _context.Database.ExecuteSqlInterpolated($"GuestsAllowed @admin_id={id}, @number_of_guests={num}");
                if (result > 0)
                {
                    TempData["Set"] = "Success";
                }
                else
                {
                    TempData["Set"] = "Fail";
                }
            }
            else
            {
                TempData["Set"] = "Fail";
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

