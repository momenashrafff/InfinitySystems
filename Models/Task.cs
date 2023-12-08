using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfinitySystems.Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Creation_Date { get; set; }
        public DateTime? Due_Date { get; set; }
        public string? Category { get; set; }
        [ForeignKey("Creator")]
        public int? Creator { get; set; }
        // public Admin? Creator { get; set; }
        public string? Status { get; set; }
        public DateTime? Reminder_Date { get; set; }
        public int? Priority { get; set; }
    }
}