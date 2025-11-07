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
        public int PageNumber { get; set; }
        public int PageSize{ get; set; }
        public Expression<Func<Product, bool>>? filter { get; set; } = null;
        public Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;
        public string? includeProperties = null;
    }
}