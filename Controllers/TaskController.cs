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
            int? Id = HttpContext.Session.GetInt32("SessionUserId");
            if (Id == null || Id.Value == -1)
            {
                return RedirectToAction("Login_Register", "Login_Register");
            }
            return RedirectToAction("Task", "Task");
        }
        public ActionResult ViewMyTask()
        {
            // Get the current user's id from the session
            int? user_id = HttpContext.Session.GetInt32("SessionUserId");

            // Get the tasks for the current user
            List<Task> tasks = GetTasksByUser(user_id.Value); // Pass the user_id to the method

            // Pass the tasks to the view
            return View(tasks);
        }

        public List<Task> GetTasksByUser(int user_id) // Change the parameter type to int
        {
            // Create a connection object to connect to the database
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Homesync;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            // Create a command object to execute the procedure
            SqlCommand cmd = new SqlCommand("viewmytask", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add a parameter for the user_id
            cmd.Parameters.AddWithValue("@user_id", user_id); // Change the parameter name and value to user_id

            // Create a list to store the tasks
            List<Task> tasks = new List<Task>();

            // Open the connection and execute the reader
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            // Loop through the reader and create task objects
            while (reader.Read())
            {
                // Create a task object with the data from the reader
                Task task = new Task();
                task.Id = reader.GetInt32(0);
                task.Name = reader.GetString(1);
                task.Creation_Date = reader.GetDateTime(2);
                task.Due_Date = reader.GetDateTime(3);
                task.Category = reader.GetString(4);
                task.Creator = reader.GetInt32(5);
                task.Status = reader.GetString(6);
                task.Reminder_Date = reader.GetDateTime(7);
                task.Priority = reader.GetInt32(8);

                // Add the task to the list
                tasks.Add(task);
            }

            // Close the reader and the connection
            reader.Close();
            conn.Close();

            // Return the list of tasks
            return tasks;
        }
        // This method shows the form to enter the task details
        public ActionResult AddTask()
        {
            // Get the current user's id from the session
            int? user_id = HttpContext.Session.GetInt32("SessionUserId");

            // Create a new task object
            Task task = new Task();

            // Pass the task object and the user id to the view
            return addfunction(task, user_id);
        }

        // This method saves the task to the database
        [HttpPost]
        public ActionResult addfunction(Task task, int? user_id) // Add the user id as a parameter
        {
            // Validate the task object exit or if nullptr
            if (ModelState.IsValid)
            {
                // Create a connection object to connect to the database
                SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Homesync;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

                // Create a command object to execute the procedure
                SqlCommand cmd = new SqlCommand("addtask", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the parameters for the task details
                cmd.Parameters.AddWithValue("@name", task.Name);
                cmd.Parameters.AddWithValue("@creation_date", task.Creation_Date);
                cmd.Parameters.AddWithValue("@due_date", task.Due_Date);
                cmd.Parameters.AddWithValue("@category", task.Category);
                cmd.Parameters.AddWithValue("@creator", task.Creator);
                cmd.Parameters.AddWithValue("@status", task.Status);
                cmd.Parameters.AddWithValue("@reminder_date", task.Reminder_Date);
                cmd.Parameters.AddWithValue("@priority", task.Priority);
                cmd.Parameters.AddWithValue("@user_id", user_id); // Pass the user id as a parameter

                // Open the connection and execute the command
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                // Redirect to the ViewMyTask action
                return RedirectToAction("Task", "Task"); // to referesh 
            }
            else
            {
                // Return the same view with the validation errors
                return RedirectToAction("Task", "Task"); // to referesh 
            }
        }

        // This method shows the confirmation message
        public ActionResult DeleteTask(int id)
        {
            // Get the current user's id from the session
            int? user_id = HttpContext.Session.GetInt32("SessionUserId");

            // Get the task by id from the database
            Task task = GetTaskById(id);

            // Pass the task and the user id to the view
            return View(task);
        }
        public Task GetTaskById(int id)
        {
            // Create a connection object to connect to the database
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Homesync;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            // Create a command object to execute the query
            SqlCommand cmd = new SqlCommand("SELECT * FROM Task WHERE Id = @id", conn);

            // Add a parameter for the id
            cmd.Parameters.AddWithValue("@id", id);

            // Create a task object to store the result
            Task task = null;

            // Open the connection and execute the reader
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Check if the reader has any rows
            if (reader.HasRows)
            {
                // Read the first row
                reader.Read();

                // Create a task object with the data from the reader
                task = new Task();
                task.Id = reader.GetInt32(0);
                task.Name = reader.GetString(1);
                task.Creation_Date = reader.GetDateTime(2);
                task.Due_Date = reader.GetDateTime(3);
                task.Category = reader.GetString(4);
                task.Creator = reader.GetInt32(5);
                task.Status = reader.GetString(6);
                task.Reminder_Date = reader.GetDateTime(7);
                task.Priority = reader.GetInt32(8);
            }

            // Close the reader and the connection
            reader.Close();
            conn.Close();

            // Return the task object
            return task;
        }

        // This method removes the task from the database
        [HttpPost]
        public ActionResult DeleteTask(Task task, int user_id) // Add the user id as a parameter
        {
            // Create a connection object to connect to the database
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Homesync;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            // Create a command object to execute the procedure
            SqlCommand cmd = new SqlCommand("deletetask", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters for the task id and the user id
            cmd.Parameters.AddWithValue("@id", task.Id);
            cmd.Parameters.AddWithValue("@user_id", user_id); // Pass the user id as a parameter

            // Open the connection and execute the command
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Redirect to the ViewMyTask action
            return RedirectToAction("ViewMyTask");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}