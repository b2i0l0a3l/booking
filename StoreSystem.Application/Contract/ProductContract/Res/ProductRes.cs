using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Application.Contract.ProductContract.Res
{
    public record ProductRes
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? SKU { get; set; } 

        public string? Barcode { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellPrice { get; set; }

        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }

        public int StoreId { get; set; }
    }
}