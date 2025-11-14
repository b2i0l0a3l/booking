using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Core.Entities;
using StoreSystem.Application.Contract.StockMovementContract.req;
using StoreSystem.Application.Contract.StockMovementContract.res;

namespace StoreSystem.Application.Contract.Common.Mappings
{
    public class StockMovementProfile: Profile
    {
        public StockMovementProfile()
        {
            CreateMap<StockMovement, StockMovementReq>().ReverseMap();
            CreateMap<StockMovement, StockMovementRes>().ReverseMap();
        }
        
    }
}