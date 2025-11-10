using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;

namespace BookingSystem.Application.Contract.ProductContract.Req
{
    public class GetProductReq
    {
       
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public ProductFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public string? IncludeProperties { get; set; }
    }

    public class ProductFilter
    {
        public string? Name { get; set; }
        public decimal? SellPrice { get; set; }
        public int? StockQuantity { get; set; }
        public decimal? CostPrice { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}