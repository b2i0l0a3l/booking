using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Core.Entities
{
    public class Category : baseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int StoreId { get; set; }


        [ForeignKey("StoreId")]
        public Store? Store { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}