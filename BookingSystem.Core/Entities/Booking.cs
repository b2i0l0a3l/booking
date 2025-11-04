using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Enums;
using BookingSystem.Core.Interfaces;

namespace BookingSystem.Core.Entities
{
    public class Booking : IEntity
    {
        
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public int? ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingTime { get; set; }
        public enStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public IApplicationUser? user { get; set; }
        public Service? Service { get; set; }
    }
}