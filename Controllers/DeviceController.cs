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

namespace InfinitySystems.Controllers
{
    public class DeviceController : Controller
    {
        private readonly HomesyncContext _context;

        public DeviceController(HomesyncContext context)
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

            bool isAdmin = HttpContext.Session.TryGetValue("IsAdmin", out byte[] isAdminBytes) &&
                            BitConverter.ToBoolean(isAdminBytes);

            if (!isAdmin)
            {
                isAdmin = _context.Admins.FromSqlRaw("SELECT * FROM Admin WHERE Admin.admin_id = {0}", userId)
                                         .SingleOrDefault() != null;
                HttpContext.Session.Set("IsAdmin", BitConverter.GetBytes(isAdmin));
            }

            ViewBag.Devices = isAdmin ? _context.Devices.FromSqlRaw("SELECT * FROM Device").ToList()
                                      : _context.Devices.FromSqlRaw($"ViewMyDevices {userId.Value}").ToList();

            ViewBag.isAdmin = isAdmin;
            return View(new Device());
        }

     [HttpPost]
        public IActionResult DeviceCharge(Device device)
        {
            int? userId = HttpContext.Session.GetInt32("SessionUserId");

            if (userId == null || userId == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }

            bool isAdmin = HttpContext.Session.TryGetValue("IsAdmin", out byte[] isAdminBytes) &&
                            BitConverter.ToBoolean(isAdminBytes);

            ViewBag.Devices = isAdmin ? _context.Devices.FromSqlRaw("SELECT * FROM Device").ToList()
                                    : _context.Devices.FromSqlRaw($"ViewMyDevices {userId.Value}").ToList();

            Console.WriteLine($"IsAdmin: {isAdmin}");
            Console.WriteLine($"ViewBag.Devices Count: {ViewBag.Devices?.Count}");

            SqlParameter deviceIdParam = new SqlParameter("@device_id", device.Id);
            SqlParameter chargeIdParam = new SqlParameter("@charge_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter locationParam = new SqlParameter("@location", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw("EXEC DeviceCharge @device_id, @charge_id OUTPUT, @location OUTPUT",
                                            deviceIdParam, chargeIdParam, locationParam);

            if (ViewBag.Devices != null && ViewBag.Devices.Count > 0 &&
                chargeIdParam.Value != DBNull.Value && locationParam.Value != DBNull.Value)
            {
                foreach (var deviceInList in ViewBag.Devices)
                {
                    if ((int)locationParam.Value == deviceInList.Room)
                    {
                        TempData["ChargeId"] = (int)chargeIdParam.Value;
                        TempData["Location"] = (int)locationParam.Value;
                        break;
                    }
                }
            }
            else
            {
                TempData["ChargeId"] = null;
                TempData["Location"] = null;
            }

            return RedirectToAction("Index", "Device", device);
        }


        [HttpPost]
        public IActionResult AddDevice(Device device)
        {
            try
            {
                SqlParameter device_idParam = new SqlParameter("@device_id", device.Id);
                SqlParameter statusParam = new SqlParameter("@status", device.Status);
                SqlParameter batteryParam = new SqlParameter("@battery", device.Battery_Status);
                SqlParameter locationParam = new SqlParameter("@location", device.Room);
                SqlParameter typeParam = new SqlParameter("@type", device.Type);

                _context.Database.ExecuteSqlRaw("EXEC AddDevice @device_id, @status, @battery, @location, @type",
                                                device_idParam, statusParam, batteryParam, locationParam, typeParam);
                TempData["AddMessage"] = "Device added successfully.";
            }
            catch (Exception)
            {
                TempData["AddError"] = "Device could not be added.";
            }

            return RedirectToAction("Index", "Device");
        }
        [HttpPost]
public IActionResult OutOfBattery(Device device)
{
    try
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "OutOfBattery";
            command.CommandType = CommandType.StoredProcedure;

            _context.Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var outOfBatteryRooms = new List<int>();

                while (reader.Read())
                {
                    outOfBatteryRooms.Add(reader.GetInt32(0));
                }

                TempData["OutOfBatteryRooms"] = outOfBatteryRooms;

                Console.WriteLine(TempData["OutOfBatteryRooms"]);
            }
        }
    }
    catch (Exception ex)
    {
        // Log the exception or handle it as needed
        Console.WriteLine($"Error in OutOfBattery action: {ex.Message}");
        TempData["OutOfBatteryRooms"] = new List<int>();
    }

    return RedirectToAction("Index", "Device");
}

    }   
}