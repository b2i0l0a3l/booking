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
using StoreSystem.Application.Common;

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
            if (entity == null )
                return GeneralResponse<int>.Failure("Invalid Data", 400);

            var result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                return GeneralResponse<int>.Failure(string.Join(" ,",errors));
                
            }
            Category category = new Category { Name = entity.Name, StoreId = entity.StoreId };
            try
            {
                await _repo.AddAsync(category);

                await _repo.SaveAsync();
                return  GeneralResponse<int>.Success(category.Id , "Category Added Successfully", 201);

            }
            catch(Exception ex)
            {
                return GeneralResponse<int>.Failure($"Error while Adding Category : {ex.Message}",500);
                
            }

        } 

        public async Task<GeneralResponse<bool?>> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return GeneralResponse<bool?>.Failure("Invalid Data", 400);
            }

            Category? category = await _repo.FindAsync(x => x.Id == id);
            if (category == null)
                return GeneralResponse<bool?>.Failure($"Category with Id : {id} Not Found", 404);

            _repo.DeleteAsync(category);
            await _repo.SaveAsync();
            return  GeneralResponse<bool?>.Success(null , "Category deleted Successfully", 200);
             
        }

        private Expression<Func<Category, bool>>?  GetFilter(GetCategoryReq entity,bool IsForStore =true)
        {
            if (entity.Filter == null)
                return null;

            Expression<Func<Category, bool>> expr = p => true;

            if (!string.IsNullOrEmpty(entity.Filter.Name))
                expr = expr.AndAlso(p => p.Name.ToLower().Contains(entity.Filter.Name.ToLower()));
            
            if (!entity.Filter.StoreId.HasValue && IsForStore)
                expr = expr.AndAlso(p => p.StoreId == entity.Filter.StoreId);

            if (entity.Filter.CreateAt.HasValue)
                expr = expr.AndAlso(p => p.CreatedAt == entity.Filter.CreateAt.Value);
            return expr;
        }
        private Func<IQueryable<Category>, IOrderedQueryable<Category>>? GetOrderBy(GetCategoryReq entity)
        {
            if (entity.OrderBy == null)
                return null;
            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null;

            if (!string.IsNullOrEmpty(entity.OrderBy))
            {
                if (entity.OrderBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.Name);
                else if (entity.OrderBy.Equals("CreateAt", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.CreatedAt);
            }
            return orderBy;
        }
        
        public async Task<GeneralResponse<PagedResult<CategoryRes>>> GetAllAsync(GetCategoryReq entity)
        {


            if (entity == null)
                return GeneralResponse<PagedResult<CategoryRes>>.Failure("Invalid Data", 400);

            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = GetOrderBy(entity);

            Expression<Func<Category, bool>>? expr = GetFilter(entity,false); ;



            PagedResult<Category> r = await _repo.GetAllAsync(entity.PageNumber, entity.PageSize, expr, orderBy, entity.IncludeProperties);
            if (r != null && r.Items.Any())
            {
                PagedResult<CategoryRes> result = new()
                {
                    Items = r.Items.Select(x => new CategoryRes { Name = x.Name, Id = x.Id, CreateAt = x.CreatedAt , StoreId = x.StoreId }),
                    PageNumber = r.PageNumber,
                    PageSize = r.PageSize,
                    TotalItems = r.TotalItems
                };
                return GeneralResponse<PagedResult<CategoryRes>>.Success(result, "success", 200);
            }
            return GeneralResponse<PagedResult<CategoryRes>>.Failure("There is no Category!", 404);

        }
        
        public async Task<GeneralResponse<PagedResult<CategoryRes>>> GetAllForStoreAsync(GetCategoryReq entity)
        {
            if (entity == null)
                return GeneralResponse<PagedResult<CategoryRes>>.Failure("Invalid Data", 400);

            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = GetOrderBy(entity);

            Expression<Func<Category, bool>>? expr = GetFilter(entity); ;



            PagedResult<Category> r = await _repo.GetAllAsync(entity.PageNumber, entity.PageSize, expr, orderBy, entity.IncludeProperties);
            if (r != null && r.Items.Any())
            {
                PagedResult<CategoryRes> result = new()
                {
                    Items = r.Items.Select(x => new CategoryRes { Name = x.Name, Id = x.Id, CreateAt = x.CreatedAt,StoreId = x.StoreId }),
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

        public async Task<GeneralResponse<bool?>> Update(CategoryReq entity, int Id)
        {
            if (Id < 1 || entity == null)
                return GeneralResponse<bool?>.Failure("Invalid Data", 400);


            var result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                return GeneralResponse<bool?>.Failure(errors, 400);
            }

            Category? category = await _repo.FindAsync(x => x.Id == Id);
            if (category == null)
                return GeneralResponse<bool?>.Failure($"Category with Id : {Id} Not Found", 404);

            category.Name = entity.Name;
            var r = await _repo.UpdateAsync(category);
            if (!r)
                return GeneralResponse<bool?>.Failure($"internal server error", 500);
            await _repo.SaveAsync();
            return GeneralResponse<bool?>.Success(null, "success", 201);

        }
   
    }
}