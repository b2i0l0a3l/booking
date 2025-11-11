using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Core.common;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using ChatApi.Application.Contract.Common;
using Microsoft.Extensions.Logging;
using StoreSystem.Application.Contract.StockMovementContract.req;
using StoreSystem.Application.Contract.StockMovementContract.res;
using StoreSystem.Application.Contract.StockMovementContract.validator;
using StoreSystem.Application.EventHandler.Product;
using StoreSystem.Application.Interfaces;
using StoreSystem.Core.Events.Product;

namespace StoreSystem.Application.Services.StockMovementService
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IReposatory<StockMovement> _repo;
        private readonly IReposatory<Product> _Productrepo;
        private ILogger<StockMovementService> _logger;
        private IMapper _Mapper;
        private StockMovementValidator _Validator;
        public StockMovementService(IReposatory<Product> Productrepo,
        IMapper Mapper,StockMovementValidator Validator,ILogger<StockMovementService> logger,IReposatory<StockMovement> repo)
        {
            _repo = repo;
            _logger = logger;
            _Validator = Validator;
            _Mapper = Mapper;
            _Productrepo = Productrepo;

        }

        public Task<GeneralResponse<bool>> CreateMovementAsync(StockMovementRes model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse<PagedResult<StockMovementRes>>> GetMovementsByProductAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}