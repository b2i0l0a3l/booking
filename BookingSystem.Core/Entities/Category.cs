using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Core.Entities
{
    public class Category : baseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;



        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}