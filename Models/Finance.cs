using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace InfinitySystems.Models
{
    [Table("Finance")]
    public class Finance
    {
        [Key]
        public int Payment_Id { get; set; }
        [ForeignKey("User_Id")]
        public int? User_Id { get; set; }
        // public Users? User_Id { get; set; }
        public string? Type { get; set; }
        public int? Amount { get; set; }
        public string? Currency { get; set; }
        public string? Method { get; set; }
        public string? Status { get; set; }
        public DateTime? Date { get; set; }
        public int? Receipt_No { get; set; }
        public DateTime? Deadline { get; set; }
        public int? Penalty { get; set; }
    }
}