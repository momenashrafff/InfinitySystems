using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InfinitySystems.Models
{
    [Table("Guest")]
    public class Guest
    {
        [ForeignKey("Guest_Id")]
        [Key]
        public int Guest_Id { get; set; }
        // public Users? Guest_Id { get; set; }
        [ForeignKey("Guest_Of")]
        public int? Guest_Of { get; set; }
        // public Admin? Guest_Of { get; set; }
        public string? Address { get; set; }
        public DateTime? Arrival_Date { get; set; }
        public DateTime? Departure_Date { get; set; }
        public string? Residential { get; set; }
    }
}