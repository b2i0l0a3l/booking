using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.Categories.req;
using BookingSystem.Application.Contract.Categories.res;
using BookingSystem.Core.common;
using BookingSystem.Core.Entities;
using ChatApi.Application.Contract.Common;

namespace BookingSystem.Application.Interfaces
{
    public interface ICategoryService 
    {
         Task<GeneralResponse<CategoryRes?>> GetByIdAsync(int id);

        Task<GeneralResponse<PagedResult<CategoryRes>>> GetAllAsync(GetCategoryReq entity);

        Task<GeneralResponse<int>> AddAsync(CategoryReq entity);

        Task<GeneralResponse<bool?>> Update(CategoryReq entity,int Id);

        Task<GeneralResponse<bool?>> DeleteAsync(int id);
    }
}