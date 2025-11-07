using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.ProductContract.Req;
using FluentValidation;

namespace BookingSystem.Application.Contract.ProductContract.Validator
{
    public class ProductValidator : AbstractValidator<ProductReq>
    {
        
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name Is Required!");
            RuleFor(p => p.StoreId).NotEmpty().WithMessage("Store Id Is Required!");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Category Id Id Is Required!");
        }
    }
}