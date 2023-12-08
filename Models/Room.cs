using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InfinitySystems.Models
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public int? Floor { get; set; }
        public string? Status { get; set; }

    }
}