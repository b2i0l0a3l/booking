using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Application.Contract.Categories.res
{
    public record CategoryRes
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
    }
}