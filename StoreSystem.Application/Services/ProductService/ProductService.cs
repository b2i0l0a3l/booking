using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.Contract.ProductContract.Req;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Application.Contract.ProductContract.Validator;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.common;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using ChatApi.Application.Contract.Common;
using Microsoft.AspNetCore.Mvc.Diagnostics;

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

        public async Task<GeneralResponse<PagedResult<ProductRes>>> GetAllAsync(GetProductReq entity)
        {
            if (entity == null)
                return GeneralResponse<PagedResult<ProductRes>>.Failure("Invalid Data");

            PagedResult<Product> pagedResult = await _repo.GetAllAsync(entity.PageNumber, entity.PageSize, entity.filter, entity.orderBy, entity.includeProperties);

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
            return GeneralResponse<PagedResult<ProductRes>>.Failure("there is no products yet!",404);
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