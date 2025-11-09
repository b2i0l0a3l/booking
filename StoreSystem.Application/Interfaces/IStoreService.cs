using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Contract.StoreContract.req;
using StoreSystem.Application.Contract.StoreContract.res;

namespace StoreSystem.Application.Interfaces
{
    public interface IStoreService
    {
        Task<GeneralResponse<StoreRes?>> GetByIdAsync(int id);

        Task<GeneralResponse<PagedResult<StoreRes>>> GetAllAsync(GetStoreReq entity);

        Task<GeneralResponse<int>> AddAsync(StoreReq entity , string UserId);

        Task<GeneralResponse<bool?>> Update(StoreReq entity,int Id);

        Task<GeneralResponse<bool?>> DeleteAsync(int id);
    }
}