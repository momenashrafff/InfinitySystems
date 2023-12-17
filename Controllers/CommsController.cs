using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;
//using InfinitySystems.Models;

namespace InfinitySystems.Controllers
{
    public class CommsController : Controller
    {
        private readonly HomesyncContext _context;

        public CommsController(HomesyncContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("SessionUserId");

            if (userId == null || userId == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }

            ViewBag.isAdmin = HttpContext.Session.GetString("SessionUserType") == "Admin";
            return View(new List<Communication>());
        }
        [HttpPost]
        public IActionResult SendMessage(int receiver_id, string title, string content)
        {
            TimeOnly timeReceived = TimeOnly.FromDateTime(DateTime.Now);

            int? userId = HttpContext.Session.GetInt32("SessionUserId");
            if (userId == null || userId.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                _context.Database.ExecuteSqlInterpolated($"SendMessage @sender_id={userId.Value}, @receiver_id={receiver_id}, @title={title}, @content={content}, @timesent={timeReceived}, @timereceived={timeReceived}");
                TempData["SendMessageMessage"] = "Send message has been done succesfullly !";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Sending message : {ex.Message}");
                TempData["SendMessageError"] = "Error while sending messages";

            }
            return RedirectToAction("Index", "Comms");

        }

        [HttpPost]
        public IActionResult DeleteMsg()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC DeleteMsg");
                TempData["DeleteMsg"] = "Last Message Deleted!";
            }
            catch (Exception ex)
            {
                TempData["DeletingMessageError"] = "No Message to delete";
            }

            return RedirectToAction("Index", "Comms");

        }

        [HttpPost]
        public IActionResult ShowMessages(int sender_id)
        {
            int? userId = HttpContext.Session.GetInt32("SessionUserId");

            if (userId == null || userId == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                List<Communication> messages = _context.Communications.FromSqlInterpolated($"ShowMessages @user_id={userId.Value}, @sender_id={sender_id}").ToList();
                bool messagesExist = messages.Count > 0;
                if (messagesExist)
                {
                    TempData["ShowMessage"] = "show message has been done succesfullly !";
                    return RedirectToAction("Index", messages);
                }
                else
                {
                    TempData["NoShowMessage"] = "No messages to show";
                }
                //TempData["ShowMessage"] = "delete message has been done succesfullly !";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Showing   message : {ex.Message}");
                TempData["ShowMessage"] = "error in showing table";
            }
            return RedirectToAction("Index", _context.Communications);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
