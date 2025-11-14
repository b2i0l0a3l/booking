using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Contract.StockContract.req;
using StoreSystem.Application.Interfaces;

namespace StoreSystem.Application.Services.StockService
{
    public class StockService : IStockService
    {
        public Task<GeneralResponse<int>> AdjustStockAsync(StockReq Req)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<int>> DecreaseStockAsync(StockReq Req)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<int>> GetCurrentStockAsync(StockReq Req)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<int>> GetLowStockProductsAsync(StockReq Req)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<int>> IncreaseStockAsync(StockReq Req)
        {
            throw new NotImplementedException();
        }
    }
}