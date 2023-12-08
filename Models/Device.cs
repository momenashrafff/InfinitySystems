using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfinitySystems.Models
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Room")]
        public int Room { get; set; }
        // public Room? Room { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public int? Battery_Status { get; set; }
    }
}