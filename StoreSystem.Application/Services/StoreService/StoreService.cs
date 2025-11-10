using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Core.common;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using BookingSystem.Infrastructure.presistence.Repo;
using ChatApi.Application.Contract.Common;
using StoreSystem.Application.Common;
using StoreSystem.Application.Contract.StoreContract.req;
using StoreSystem.Application.Contract.StoreContract.res;
using StoreSystem.Application.Contract.StoreContract.validator;
using StoreSystem.Application.Interfaces;

namespace StoreSystem.Application.Services.StoreService
{
    public class StoreService: IStoreService
    {
        private readonly IReposatory<Store> _repo;
        private readonly IMapper _mapper;
        private readonly StoreValidator _validator;

        public StoreService(IReposatory<Store> repo, IMapper mapper, StoreValidator validator)
        {
            _repo = repo;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<GeneralResponse<int>> AddAsync(StoreReq entity,string UserId)
        {

             if (entity == null)
                return GeneralResponse<int>.Failure("Invalid Data");
            try
            {
                var result = _validator.Validate(entity);

                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                    return GeneralResponse<int>.Failure(string.Join(", ", errors));
                }


                Store store = _mapper.Map<Store>(entity);
                store.UserId = UserId;
                await _repo.AddAsync(store);
                await _repo.SaveAsync();
                return GeneralResponse<int>.Success(store.Id, "Store Added Successfully",201);
                
            }catch(Exception ex)
            {
                    return GeneralResponse<int>.Failure(ex.Message);
                
            }
        }

        public async Task<GeneralResponse<bool?>> DeleteAsync(int id)
        {
            if (id < 1)
                return GeneralResponse<bool?>.Failure("Invalid Data");

            var result = await _repo.FindAsync(x => x.Id == id);
            if (result == null)
                return GeneralResponse<bool?>.Failure($"store With Id {id} Not Found", 404);
            
            _repo.DeleteAsync(result);
            await _repo.SaveAsync();
            return GeneralResponse<bool?>.Success(null, "store deleted Successfully",201);
        }
    private Expression<Func<Store, bool>>?  GetFilter(GetStoreReq entity)
        {
            if (entity.Filter == null)
                return null;

            Expression<Func<Store, bool>> expr = p => true;

            if (!string.IsNullOrEmpty(entity.Filter.Name))
                expr = expr.AndAlso(p => p.Name.Contains(entity.Filter.Name, StringComparison.OrdinalIgnoreCase));

            if (entity.Filter.CreateAt.HasValue)
                expr = expr.AndAlso(p => p.CreatedAt == entity.Filter.CreateAt.Value);
            return expr;
        }
          private Func<IQueryable<Store>, IOrderedQueryable<Store>>?  GetOrderBy(GetStoreReq entity)
        {
            if (entity.OrderBy == null)
                return null;
            Func<IQueryable<Store>, IOrderedQueryable<Store>>? orderBy = null;

            if (!string.IsNullOrEmpty(entity.OrderBy))
            {
                if (entity.OrderBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.Name);
                else if (entity.OrderBy.Equals("CreateAt", StringComparison.OrdinalIgnoreCase))
                    orderBy = q => q.OrderBy(s => s.CreatedAt);
            }
            return orderBy;
        }
        public async Task<GeneralResponse<PagedResult<StoreRes>>> GetAllAsync(GetStoreReq entity)
        {
            if (entity == null)
                return GeneralResponse<PagedResult<StoreRes>>.Failure("Invalid Data");


            Expression<Func<Store, bool>>? expr = GetFilter(entity);
            Func<IQueryable<Store>, IOrderedQueryable<Store>>? orderBy = GetOrderBy(entity);
           

            try
            {


                PagedResult<Store> pagedResult = await _repo.GetAllAsync(entity.PageNumber, entity.PageSize, expr, orderBy, entity.IncludeProperties ?? null);

                if (pagedResult != null)
                {
                    PagedResult<StoreRes> result = new()
                    {
                        Items = pagedResult.Items.Select(x => _mapper.Map<StoreRes>(x)).ToList(),
                        PageNumber = pagedResult.PageNumber,
                        PageSize = pagedResult.PageSize
                    };
                    return GeneralResponse<PagedResult<StoreRes>>.Success(result, "Success", 200);
                }
                return GeneralResponse<PagedResult<StoreRes>>.Failure("there is no products yet!", 404);
            }
            catch (Exception ex)
            {
                return GeneralResponse<PagedResult<StoreRes>>.Failure("Error Happend");
            }
        }

        public async Task<GeneralResponse<StoreRes?>> GetByIdAsync(int id)
        {
             if (id < 1)
                return GeneralResponse<StoreRes?>.Failure("Invalid Data");

            Store? store = await _repo.FindAsync(x => x.Id == id);
            if (store != null)
                return GeneralResponse<StoreRes?>.Success(_mapper.Map<StoreRes>(store), "success", 200);
            
            return GeneralResponse<StoreRes?>.Failure($"there is no store with that Id {id} ");
            
        }

        public async Task<GeneralResponse<bool?>> Update(StoreReq entity, int Id)
        {
            if (Id < 1 || entity == null)
                return GeneralResponse<bool?>.Failure("Invalid Data");


            var result = _validator.Validate(entity);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                return GeneralResponse<bool?>.Failure(string.Join(", ", errors));
            }

            Store? store = await _repo.FindAsync(x => x.Id == Id);

            if (store == null)
                return GeneralResponse<bool?>.Failure($"there is no Store with that Id : {Id}");

            _mapper.Map(entity, store);
            await _repo.SaveAsync();
            return GeneralResponse<bool?>.Success(null, "Store Updated Successfully",200);
            
        }
    }
}