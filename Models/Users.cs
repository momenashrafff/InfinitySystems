using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace InfinitySystems.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int ID { get; set; }

        public string? F_Name { get; set; }

        public string? L_Name { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Preferences { get; set; }

        [ForeignKey("Room")]
        public int? Room { get; set; }
        // public Room? Room { get; set; }

        public string? Type { get; set; }

        public DateTime? Birth_Date { get; set; }

        public int? Age { get; set; }
    }
}