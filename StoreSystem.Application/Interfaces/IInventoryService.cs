using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Contract.InventoryContract.req;
using StoreSystem.Application.Contract.InventoryContract.res;
using StoreSystem.Application.Contract.StockContract.res;
using StoreSystem.Core.Entities;

namespace StoreSystem.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<GeneralResponse<int>> AddInventoryAsync(InventoryReq model);
        Task<GeneralResponse<bool?>> UpdateInventoryAsync(InventoryReq model);
        Task<GeneralResponse<bool?>> DeleteInventoryAsync(int inventoryId);
        Task<GeneralResponse<InventoryRes>> GetInventoryByIdAsync(int inventoryId);
        Task<GeneralResponse<PagedResult<InventoryRes>>> GetAllInventoriesAsync(GetAllInventoryReq Model);
        Task<GeneralResponse<IEnumerable<PagedResult<StockRes>>>> GetInventoryStockAsync(int inventoryId);
    }
}