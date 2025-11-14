using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Application.Contract.StockContract.req
{
    public class StockReq
    {
        public int Id { get; set; }
        public DateTime CreateAt{ get; set; }
        public int productId { get; set; }
        public int inventoryId { get; set; }
        public decimal quantity { get; set; }
        public string? reason { get; set; }
    }
}