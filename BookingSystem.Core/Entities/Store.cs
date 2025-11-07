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

        public ICollection<IApplicationUser> Users { get; set; } = new List<IApplicationUser>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    
        public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
        public ICollection<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();
    }
}