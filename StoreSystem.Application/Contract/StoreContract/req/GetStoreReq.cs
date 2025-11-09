using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;

namespace StoreSystem.Application.Contract.StoreContract.req
{
    public record GetStoreReq
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public StoreFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public string? IncludeProperties { get; set; }
    }

    public class StoreFilter
    {
        public string? Name { get; set; }
        public DateTime? CreateAt { get; set; }
    }

}