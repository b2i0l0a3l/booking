using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;

namespace BookingSystem.Application.Contract.Categories.req
{
    public record GetCategoryReq
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public CategoryFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public string? IncludeProperties { get; set; }
    }

    public class CategoryFilter
    {
        public int? StoreId{ get; set; }
        public string? Name { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}