using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChatApi.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser,IApplicationUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string FullName => $"{FirstName} {LastName}";
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public IEnumerable<Store?> Store { get; set; } = new List<Store?>();
    }
}