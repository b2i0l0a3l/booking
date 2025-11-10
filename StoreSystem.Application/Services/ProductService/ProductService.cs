using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.Contract.Categories.req;
using BookingSystem.Application.Contract.ProductContract.Req;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Application.Contract.ProductContract.Validator;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.common;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using ChatApi.Application.Contract.Common;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using StoreSystem.Application.Common;

namespace StoreSystem.Application.Services.ProductService
{
    public class ProductService(IReposatory<Product> _repo, ProductValidator _validator,IMapper _mapper) : IProductService
    {
        public async Task<GeneralResponse<int>> AddAsync(ProductReq entity)
        {
            if (entity == null)
                return GeneralResponse<int>.Failure("Invalid Data");

            var result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                return GeneralResponse<int>.Failure(string.Join(", ", errors));
            }

           
            Product product = _mapper.Map<Product>(entity);
            await _repo.AddAsync(product);
            await _repo.SaveAsync();
            return GeneralResponse<int>.Success(product.Id, "Product Added Successfully",201);
        }

        public async Task<GeneralResponse<bool?>> DeleteAsync(int id)
        {
            if (id < 1)
                return GeneralResponse<bool?>.Failure("Invalid Data");

            var result = await _repo.FindAsync(x => x.Id == id);
            if (result == null)
                return GeneralResponse<bool?>.Failure($"Product With Id {id} Not Found", 404);

            _repo.DeleteAsync(result);
            await _repo.SaveAsync();
            return GeneralResponse<bool?>.Success(null, "Product deleted Successfully",201);
        }

        private Expression<Func<Product, bool>>? GetFilter(GetProductReq entity)
        {
            if (entity.Filter == null)
                return null;

            Expression<Func<Product, bool>> expr = p => true;

            if (!string.IsNullOrEmpty(entity.Filter.Name))
                expr = expr.AndAlso(p => p.Name.ToLower().Contains(entity.Filter.Name.ToLower()));

            if (entity.Filter.CreateAt.HasValue)
                expr = expr.AndAlso(p => p.CreatedAt == entity.Filter.CreateAt.Value);

            if (entity.Filter.UpdateAt.HasValue)
                expr = expr.AndAlso(p => p.UpdateAt == entity.Filter.UpdateAt.Value);

            if (entity.Filter.CostPrice.HasValue)
                expr = expr.AndAlso(p => p.CostPrice == entity.Filter.CostPrice.Value);

            if (entity.Filter.SellPrice.HasValue)
                expr = expr.AndAlso(p => p.SellPrice == entity.Filter.SellPrice.Value);

            if (entity.Filter.StockQuantity.HasValue)
                expr = expr.AndAlso(p => p.StockQuantity == entity.Filter.StockQuantity.Value);

            return expr;
        }

          private void GetOrderBy(ref Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy,GetProductReq entity)
        {

            if (!string.IsNullOrEmpty(entity.OrderBy))
            {
                if (entity.OrderBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.Name);
                else if (entity.OrderBy.Equals("CreateAt", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.CreatedAt);
                else if (entity.OrderBy.Equals("UpdateAt", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.UpdateAt);
                else if (entity.OrderBy.Equals("CostPricePrice", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.CostPrice);
                else if (entity.OrderBy.Equals("StockQuantity", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.StockQuantity);
                else if (entity.OrderBy.Equals("SellPrice", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.SellPrice);
            }
        }

        public async Task<GeneralResponse<PagedResult<ProductRes>>> GetAllAsync(GetProductReq entity)
        {
            if (entity == null)
                return GeneralResponse<PagedResult<ProductRes>>.Failure("Invalid Data");


            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;
            Expression<Func<Product, bool>>? expr = GetFilter(entity);

            GetOrderBy(ref orderBy, entity);

            PagedResult<Product> pagedResult = await _repo.GetAllAsync(entity.PageNumber, entity.PageSize, expr, orderBy, entity.IncludeProperties);

            if (pagedResult != null)
            {
                PagedResult<ProductRes> result = new()
                {
                    Items = pagedResult.Items.Select(x => _mapper.Map<ProductRes>(x)).ToList(),
                    PageNumber = pagedResult.PageNumber,
                    PageSize = pagedResult.PageSize
                };
                return GeneralResponse<PagedResult<ProductRes>>.Success(result, "Success", 200);
            }
            return GeneralResponse<PagedResult<ProductRes>>.Failure("there is no products yet!", 404);
        }

        public async Task<GeneralResponse<ProductRes?>> GetByIdAsync(int id)
        {
            if (id < 1)
                return GeneralResponse<ProductRes?>.Failure("Invalid Data");

            Product? product = await _repo.FindAsync(x => x.Id == id);
            if (product != null)
                return GeneralResponse<ProductRes?>.Success(_mapper.Map<ProductRes>(product), "success", 200);
            
            return GeneralResponse<ProductRes?>.Failure($"there is no product with that Id {id} ");
            
        }

        public async Task<GeneralResponse<bool?>> Update(ProductReq entity, int Id)
        {
            if (Id < 1 || entity == null)
                return GeneralResponse<bool?>.Failure("Invalid Data");


            var result = _validator.Validate(entity);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                return GeneralResponse<bool?>.Failure(string.Join(", ", errors));
            }

            Product? product = await _repo.FindAsync(x => x.Id == Id);

            if (product == null)
                return GeneralResponse<bool?>.Failure($"there is no product with that Id : {Id}");

            _mapper.Map(entity, product);
            await _repo.SaveAsync();
            return GeneralResponse<bool?>.Success(null, "Product Updated Successfully",200);
            
        }
    }
}