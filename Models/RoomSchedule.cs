using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfinitySystems.Models
{
    [Table("Room_schedule")]
    public class RoomSchedule
    {
        [ForeignKey("Creator_Id")]
        public int? Creator_Id { get; set; }
        // public Users? Creator_Id { get; set; }
        public string? Action { get; set; }
        [ForeignKey("Room")]
        public int Room { get; set; }
        // public Room? Room { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime? End_Time { get; set; }
    }
}