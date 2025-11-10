using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.Contract.Categories.req;
using BookingSystem.Core.Entities;
using FluentValidation;

namespace BookingSystem.Application.Contract.Categories.Validator
{
    public class CategoryValidator : AbstractValidator<CategoryReq>

    {
            public CategoryValidator()
            {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MaximumLength(30);

            RuleFor(p => p.StoreId)
                .NotEmpty().NotNull().GreaterThan(0).WithMessage("StoreId is Required");
                

            }

        
    }
}