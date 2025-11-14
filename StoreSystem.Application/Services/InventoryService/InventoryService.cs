using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Contract.InventoryContract.req;
using StoreSystem.Application.Contract.InventoryContract.res;
using StoreSystem.Application.Contract.StockContract.res;
using StoreSystem.Application.Interfaces;

namespace StoreSystem.Application.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        public Task<GeneralResponse<int>> AddInventoryAsync(InventoryReq model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<bool?>> DeleteInventoryAsync(int inventoryId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<PagedResult<InventoryRes>>> GetAllInventoriesAsync(GetAllInventoryReq Model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<InventoryRes>> GetInventoryByIdAsync(int inventoryId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<IEnumerable<PagedResult<StockRes>>>> GetInventoryStockAsync(int inventoryId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<bool?>> UpdateInventoryAsync(InventoryReq model)
        {
            throw new NotImplementedException();
        }
    }
}