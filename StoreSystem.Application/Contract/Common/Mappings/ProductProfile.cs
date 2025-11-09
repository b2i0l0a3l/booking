using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.Contract.ProductContract.Req;
using BookingSystem.Application.Contract.ProductContract.Res;
using BookingSystem.Core.Entities;

namespace StoreSystem.Application.Contract.Common.Mappings
{
    public class ProductProfile : Profile
    {
        
        public ProductProfile()
        {
            CreateMap<Product, ProductRes>().ReverseMap();
            CreateMap<Product, ProductReq>().ReverseMap();
        }
    }
}