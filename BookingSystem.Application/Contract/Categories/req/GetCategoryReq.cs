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
        public int PageNumber { get; set; }
        public int PageSize{ get; set; }
        public Expression<Func<Category, bool>>? filter { get; set; } = null;
        public Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null;
        public string? includeProperties = null;
    }
}