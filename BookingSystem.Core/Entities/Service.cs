using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingSystem.Core.Entities
{
    public class Service
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = "";
        public int Duration { get; set; } 
        public decimal Price { get; set; }

        public ICollection<Booking> bookings { get; set; } = new List<Booking>();
    }
}