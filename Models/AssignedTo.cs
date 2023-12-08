using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InfinitySystems.Models
{
    [Table("Assigned_to")]
    public class AssignedTo
    {
        [ForeignKey("Admin_Id")]
        public int? Admin_Id { get; set; }
        // public Admin? Admin_Id { get; set; }
        [ForeignKey("Task_Id")]
        public int Task_Id { get; set; }
        // public Task? Task_Id { get; set; }
        [ForeignKey("User_Id")]
        public string? User_Id { get; set; }
        // public Users? User_Id { get; set; }

    }
}