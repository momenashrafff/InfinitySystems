using InfinitySystems.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace InfinitySystems.Controllers
{
    public class Login_RegisterController : Controller
    {
        private readonly IConfiguration _configuration;

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
                return View(user);
            }
            else
            {
                ViewBag.LoginStatus = 1;

                int Id = Convert.ToInt32(user_id.Value);
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Login_Register", "Login_Register");
            }
            else
            {
                ViewBag.SignUpStatus = 1;
                int Id = Convert.ToInt32(user_id.Value);
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}