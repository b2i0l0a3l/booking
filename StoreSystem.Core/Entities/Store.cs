using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Interfaces;

namespace BookingSystem.Core.Entities
{
    public class Store : baseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public string UserId { get; set; } = string.Empty;
        public IApplicationUser? Users { get; set; }

        public ICollection<StockMovement> stockMovements { get; set; } = new List<StockMovement>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Category> categories { get; set; } = new List<Category>();
        public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
        public ICollection<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();
    }
}