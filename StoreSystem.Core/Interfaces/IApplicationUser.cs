using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;

namespace BookingSystem.Core.Interfaces
{
    public interface IApplicationUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string FullName => $"{FirstName} {LastName}";
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; } 
        public IEnumerable<Store?> Store { get; set; } 


        
    }
}