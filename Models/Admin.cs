using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Server.HttpSys;

namespace InfinitySystems.Models
{
    [Table("Admin")]
    public class Admin
    {
        [ForeignKey("Admin_Id")]
        [Key]
        public int Admin_Id { get; set; }
        // public Users? Admin_Id { get; set; }
        public int? No_Of_Guests_Allowed { get; set; }
        public int? Salary { get; set; }
    }
}