using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InfinitySystems.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Task = InfinitySystems.Models.Task;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Threading.Tasks;
using Humanizer;

namespace InfinitySystems.Controllers
{
    public class TaskController : Controller
    {
        private readonly HomesyncContext _context;

        public TaskController(HomesyncContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Task task = new Task();

            int? Id = HttpContext.Session.GetInt32("SessionUserId");

            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            int? user_id = HttpContext.Session.GetInt32("SessionUserId");
            var result = _context.Tasks.FromSqlRaw("EXEC ViewMyTask @user_id={0}", user_id).ToList();
            ViewBag.tasks = result;
            return View(task);
        }
        [HttpPost]
        public IActionResult FinishTask(string title)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            int result = _context.Database.ExecuteSqlInterpolated($"FinishMyTask @user_id={Id.Value}, @title={title}");
            TempData["FinishTask"] = "Task Finished successfully!";
            return RedirectToAction("Index", "Task");
        }
        [HttpPost]
        public IActionResult AddReminder(int taskid, DateTime ReminderDate)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            int result = _context.Database.ExecuteSqlInterpolated($"AddReminder @user_id={Id.Value}, @task_id={taskid}, @reminder={ReminderDate}");
            if (result > 0)
            {
                TempData["AddReminder"] = "Done successfully!";
            }
            else
            {
                TempData["AddReminder"] = "failed !";
            }
            return RedirectToAction("Index", "Task");
        }
        [HttpPost]
        public IActionResult UpdateTaskDeadline(int taskid, DateTime deadline)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            int result = _context.Database.ExecuteSqlInterpolated($"UpdateTaskDeadline @user_id={Id.Value}, @deadline ={deadline}, @task_id={taskid}");
            if (result > 0)
            {
                TempData["UpdateTaskDeadline"] = "Done successfully!";
            }
            else
            {
                TempData["UpdateTaskDeadline"] = "failed !";
            }
            return RedirectToAction("Index", "Task");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}