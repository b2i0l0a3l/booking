using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.Categories.req;
using BookingSystem.Application.Contract.Categories.res;
using BookingSystem.Application.Contract.Categories.Validator;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.common;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using ChatApi.Application.Contract.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingSystem.Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IReposatory<Category> _repo;
        private CategoryValidator _validator;
        public CategoryService(IReposatory<Category> repo, CategoryValidator validations)
        {
            _repo = repo;
            _validator = validations;

        }

        public async Task<GeneralResponse<int>> AddAsync(CategoryReq entity)
        {
            if (entity == null)
                return GeneralResponse<int>.Failure("Invalid Data", 400);



            var result = _validator.Validate(entity);

            if(!result.IsValid)
                return GeneralResponse<int>.Failure(string.Join(" ,",result.Errors), 400);
            Category category = new Category { Name = entity.Name };
            await _repo.AddAsync(new Category { Name = entity.Name });

            await _repo.SaveAsync();
            return  GeneralResponse<int>.Success(category.Id , "Category Added Successfully", 201);

        } 

        public async Task<GeneralResponse<bool?>> DeleteAsync(int id)
        {
            if (id < 1)
                return GeneralResponse<bool?>.Failure("Invalid Data", 400);

            Category? category = await _repo.FindAsync(x => x.Id == id);
            if (category == null)
                return GeneralResponse<bool?>.Failure($"Category with Id : {id} Not Found", 404);

            _repo.DeleteAsync(category);
            return  GeneralResponse<bool?>.Success(null , "Category deleted Successfully", 200);
             
        }

        public async Task<GeneralResponse<PagedResult<CategoryRes>>> GetAllAsync(GetCategoryReq entity)
        {
            if(entity == null)
                return GeneralResponse<PagedResult<CategoryRes>>.Failure("Invalid Data", 400);
            PagedResult<Category> r = await _repo.GetAllAsync(entity.PageNumber, entity.PageSize, entity.filter, entity.orderBy, entity.includeProperties);
            if (r != null)
            {
                PagedResult<CategoryRes> result = new()
                {
                    Items = r.Items.Select(x => new CategoryRes { Name = x.Name, Id = x.Id }),
                    PageNumber = r.PageNumber,
                    PageSize = r.PageSize,
                    TotalItems = r.TotalItems

                };
                return GeneralResponse<PagedResult<CategoryRes>>.Success(result, "success", 200);
            }
                return GeneralResponse<PagedResult<CategoryRes>>.Failure("There is no Category!", 404);
                
        }

        public async Task<GeneralResponse<CategoryRes?>> GetByIdAsync(int id)
        {
             if (id < 1)
                return GeneralResponse<CategoryRes?>.Failure("Invalid Data", 400);

            Category? category = await _repo.FindAsync(x => x.Id == id);
            if (category == null)
                return GeneralResponse<CategoryRes?>.Failure($"Category with Id : {id} Not Found", 404);
            return  GeneralResponse<CategoryRes?>.Success(new CategoryRes{Name = category.Name,Id = category.Id} , "success", 201);
        }

        public async Task<GeneralResponse<bool?>> Update(CategoryReq entity,int Id)
        {
            if(Id < 1 || entity == null)
                return GeneralResponse<bool?>.Failure("Invalid Data", 400);


            var result = _validator.Validate(entity);

            if(!result.IsValid)
                return GeneralResponse<bool?>.Failure(string.Join(" ,",result.Errors), 400);
            Category? category = await _repo.FindAsync(x => x.Id == Id);
            if (category == null)
                return GeneralResponse<bool?>.Failure($"Category with Id : {Id} Not Found", 404);

            var r = await _repo.UpdateAsync(category);
            if(!r)
                return GeneralResponse<bool?>.Failure($"internal server error", 500);
            return  GeneralResponse<bool?>.Success(null , "success", 201);

        }
    }
}