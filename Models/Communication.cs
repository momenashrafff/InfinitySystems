using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfinitySystems.Models
{
    [Table("Communication")]
    public class Communication
    {
        [Key]
        public int Message_Id { get; set; }
        [ForeignKey("Sender_Id")]
        public string? Sender_Id { get; set; }
        // public Users? Sender_Id { get; set; }
        [ForeignKey("Receiver_Id")]
        public string? Receiver_Id { get; set; }
        // public Users? Receiver_Id { get; set; }
        public string? Content { get; set; }
        public TimeOnly? Time_Sent { get; set; }
        public TimeOnly? Time_Received { get; set; }
        public TimeOnly? Time_Read { get; set; }
        public string? Title { get; set; }
    }
}