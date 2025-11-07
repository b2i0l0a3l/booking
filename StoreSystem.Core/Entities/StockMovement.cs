using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.enums;

namespace BookingSystem.Core.Entities
{
    public class StockMovement : baseEntity
    {
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = new Product();

        public int Qty { get; set; }

        public MovementType Type { get; set; }

        public int? ReferenceId { get; set; }

        public DateTime Date { get; set; }
        
        [MaxLength(255)]
        public string? Note { get; set; }
    }
}