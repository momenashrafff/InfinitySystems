using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InfinitySystems.Models
{
    [Table("Calendar")]
    public class Calendar
    {
        public int Event_Id { get; set; }
        [ForeignKey("User_Assigned_To")]
        public int? User_Assigned_To { get; set; }
        // public Users? User_Assigned_To { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? Reminder_Date { get; set; }
    }
}