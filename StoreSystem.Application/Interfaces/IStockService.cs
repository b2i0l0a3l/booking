using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Contract.StockContract.req;

namespace StoreSystem.Application.Interfaces
{
    public interface IStockService
    {
        Task<GeneralResponse<int>> IncreaseStockAsync(StockReq Req); 
        Task<GeneralResponse<int>> DecreaseStockAsync(StockReq Req); 
        Task<GeneralResponse<int>> AdjustStockAsync(StockReq Req); 
        Task<GeneralResponse<int>> GetCurrentStockAsync(StockReq Req);
        Task<GeneralResponse<int>> GetLowStockProductsAsync(StockReq Req); 
    }
}