using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;

namespace InfinitySystems.Controllers
{
    public class FinanceController : Controller
    {
        private readonly HomesyncContext _context;

        public FinanceController(HomesyncContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("SessionUserId");
            if (userId == null || userId.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            bool isAdmin = HttpContext.Session.TryGetValue("IsAdmin", out byte[] isAdminBytes) &&
                            BitConverter.ToBoolean(isAdminBytes);

            if (!isAdmin)
            {
                isAdmin = _context.Admins.FromSqlRaw("SELECT * FROM Admin WHERE Admin.admin_id = {0}", userId)
                                         .SingleOrDefault() != null;
                HttpContext.Session.Set("IsAdmin", BitConverter.GetBytes(isAdmin));
            }

            ViewBag.isAdmin = isAdmin;
            return View(new Finance());
        }
        [HttpPost]
        public IActionResult ReceiveMoney(int sender_id, string type, int amount, string status, DateTime date)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"ReceiveMoney @sender_id={sender_id}, @type={type}, @amount={amount}, @status={status}, @date={date}");
                TempData["ReceiveMoneyMessage"] = "Money Has been received succesfullly !";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Receiving money: {ex.Message}");
                TempData["ReceiveMoneyError"] = "Error while Receiving money.";
            }
            return RedirectToAction("Index", "Finance");

        }
        [HttpPost]
        public IActionResult PlanPayment(int sender_id, int receiver_id, int amount, string status, DateTime deadline)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"PlanPayment @sender_id={sender_id}, @reciever_id={receiver_id}, @amount={amount}, @status={status}, @deadline={deadline}");
                TempData["PlanPaymentMessage"] = "Plan payment has been done succesfullly !";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Creating a payment : {ex.Message}");
                TempData["PlanPaymentError"] = "Error while Creating a payment.";
            }
            return RedirectToAction("Index", "Finance");
        }
        [HttpPost]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}