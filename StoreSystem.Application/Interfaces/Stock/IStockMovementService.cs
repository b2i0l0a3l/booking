using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.ProductContract.Req;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Contract.StockMovementContract.req;
using StoreSystem.Application.Contract.StockMovementContract.res;
using StoreSystem.Core.Events.Product;

namespace StoreSystem.Application.Interfaces
{
    public interface IStockMovementService
    {
        Task<GeneralResponse<bool>> CreateMovementAsync(StockMovementRes model);       
        Task<GeneralResponse<PagedResult<StockMovementRes>>> GetMovementsByProductAsync(int productId);
    
    }
}