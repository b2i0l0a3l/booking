using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.ProductContract.Req;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Core.common;
using ChatApi.Application.Contract.Common;

namespace BookingSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<GeneralResponse<ProductRes?>> GetByIdAsync(int id);

        Task<GeneralResponse<PagedResult<ProductRes>>> GetAllAsync(GetProductReq entity);

        Task<GeneralResponse<int>> AddAsync(ProductReq entity);

        Task<GeneralResponse<bool?>> Update(ProductReq entity,int Id);

        Task<GeneralResponse<bool?>> DeleteAsync(int id);
    }
}