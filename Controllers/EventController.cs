using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

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
            ViewBag.isAdmin = HttpContext.Session.GetString("SessionUserType") == "Admin";
            return View(calendar);
        }

        [HttpPost]
        [Route("CreateEvent")]
        public IActionResult CreateEvent(Calendar calendar)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                int result = _context.Database.ExecuteSqlInterpolated($"CreateEvent @event_id={calendar.Event_Id}, @user_id ={Id.Value}, @name ={calendar.Name}, @location ={calendar.Location}, @description ={calendar.Location}, @reminder_date={calendar.Reminder_Date}, @other_user_id={calendar.User_Assigned_To}");
                if (result > 0)
                {
                    TempData["CMessage"] = "Event Has Been Created Successfully";
                }
                else
                {
                    TempData["CMessage"] = "Event Hasn't Been Created Successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["CMessage"] = "An error occurred while creating the event.";
            }
            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        [Route("AssignEvent")]
        public IActionResult AssignEvent(Calendar calendar)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                int result = _context.Database.ExecuteSqlInterpolated($"AssignUser @event_id={calendar.Event_Id}, @user_id ={calendar.User_Assigned_To}");
                if (result > 0)
                {
                    TempData["AsMessage"] = "Person Assigned Successfully";
                }
                else
                {
                    TempData["AsMessage"] = "Person Wasn't Assigned Successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["AsMessage"] = "An error occurred while assigning the person.";
            }
            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        [Route("UninviteEvent")]
        public IActionResult UninviteEvent(Calendar calendar)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                int result = _context.Database.ExecuteSqlInterpolated($"Uninvited @event_id={calendar.Event_Id}, @user_id ={calendar.User_Assigned_To}");
                if (result > 0)
                {
                    TempData["INVMessage"] = "User Uninvited To Event Successfully";
                }
                else
                {
                    TempData["INVMessage"] = "User Was Uninvited To Event InSuccessfully";
                }
            }
            catch (Exception ex)
            {
                TempData["INVMessage"] = "An error occurred while uninviting the user.";
            }
            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        [Route("SearchEvent")]
        public IActionResult SearchEvent(Calendar calendar)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                var events = _context.Calendars.FromSqlInterpolated($"EXECUTE ViewEvent @event_id={calendar.Event_Id}, @user_id ={Id.Value}").ToList();
                ViewBag.Events = events;
            }
            catch (Exception ex)
            {
                ViewBag.Events = "Couldnt Find Events Assigned To User";
            }
            return View("Index");
        }


        [HttpPost]
        [Route("RemoveEvent")]
        public IActionResult RemoveEvent(Calendar calendar)
        {
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            try
            {
                int result = _context.Database.ExecuteSqlInterpolated($"Uninvited @event_id={calendar.Event_Id}, @user_id ={Id.Value}");
                if (result > 0)
                {
                    TempData["REMessage"] = "Event Removed Successfully";
                }
                else
                {
                    TempData["REMessage"] = "Event wasn't Removed Successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["REMessage"] = "An error occurred while removing the event.";
            }
            return RedirectToAction("Index", "Event");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}