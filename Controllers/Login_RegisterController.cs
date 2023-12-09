using InfinitySystems.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InfinitySystems.Controllers
{
    public class Login_RegisterController : Controller
    {
        private readonly IConfiguration _configuration;
        private const int SessionUserId = -1;

        // private readonly ILogin _loginUser;

        public Login_RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;

            // _loginUser = loguser;
        }
        [HttpGet]
        // [Route("Login")]
        public IActionResult Login_Register()
        {
            Users user = new();
            HttpContext.Session.SetInt32("SessionUserId", -1);
            ViewBag.Id = -1;
            return View(user);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Users user)
        {
            string Email = user.Email;
            string Password = user.Password;
            // var issuccess = _loginUser.AuthenticateUser(Email, Password);
            string connection = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand login = new SqlCommand("UserLogin", sqlConnection);
            login.CommandType = System.Data.CommandType.StoredProcedure;
            login.Parameters.Add(new SqlParameter("@email", Email));
            login.Parameters.Add(new SqlParameter("@password", Password));

            SqlParameter success = login.Parameters.Add(@"success", SqlDbType.Bit);
            success.Direction = ParameterDirection.Output;
            SqlParameter user_id = login.Parameters.Add(@"user_id", SqlDbType.Int);
            user_id.Direction = ParameterDirection.Output;
            sqlConnection.Open();
            login.ExecuteNonQuery();
            sqlConnection.Close();
            // Response.WriteAsync(success.Value.ToString());

            if (success.Value.ToString().Equals("False"))
            {
                ViewBag.LoginStatus = 0;
                ViewBag.Id = -1;
                return RedirectToAction("Login_Register", "Login_Register");
            }
            else
            {
                HttpContext.Session.SetInt32("SessionUserId", Convert.ToInt32(user_id.Value));
                ViewBag.LoginStatus = 1;
                return RedirectToAction("HomePage", "HomePage");
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(Users user)
        {
            string Mail = user.Email;
            string FName = user.F_Name;
            string LName = user.L_Name;
            DateTime? BirthDate = user.Birth_Date;
            string Pass = user.Password;
            string connection = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand signup = new SqlCommand("UserRegister", sqlConnection);
            signup.CommandType = System.Data.CommandType.StoredProcedure;
            signup.Parameters.Add(new SqlParameter("@email", Mail));
            signup.Parameters.Add(new SqlParameter("@first_name", FName));
            signup.Parameters.Add(new SqlParameter("@last_name", LName));
            signup.Parameters.Add(new SqlParameter("@birth_date", BirthDate));
            signup.Parameters.Add(new SqlParameter("@password", Pass));

            SqlParameter user_id = signup.Parameters.Add(@"user_id", SqlDbType.Int);
            user_id.Direction = ParameterDirection.Output;
            sqlConnection.Open();
            signup.ExecuteNonQuery();
            sqlConnection.Close();
            // Response.WriteAsync(success.Value.ToString());

            if (user_id.Value.ToString().Equals("-1"))
            {
                ViewBag.SignUpStatus = 0;
                ViewBag.Id = -1;
                return RedirectToAction("Login_Register", "Login_Register");
            }
            else
            {
                HttpContext.Session.SetInt32("SessionUserId", Convert.ToInt32(user_id.Value));
                ViewBag.SignUpStatus = 1;
                return RedirectToAction("HomePage", "HomePage");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}