using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BookingSystem.Application.Contract.Categories.Validator;
using BookingSystem.Application.Contract.ProductContract.Validator;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services.CategoryService;
using ChatApi.Application.Interfaces;
using ChatApi.Application.Interfaces.Auth;
using ChatApi.Application.Services.authService;
using ChatApi.Application.Services.AuthService.Login;
using ChatApi.Application.Services.AuthService.Refresh;
using ChatApi.Application.Services.AuthService.Register;
using ChatApi.Application.Services.TokenService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Application.Contract.StockMovementContract.validator;
using StoreSystem.Application.Contract.StoreContract.validator;
using StoreSystem.Application.EventHandler.Product;
using StoreSystem.Application.Interfaces;
using StoreSystem.Application.Services.ProductService;
using StoreSystem.Application.Services.StockMovementService;
using StoreSystem.Application.Services.StoreService;

namespace ChatApi.Application.ServiceRegistration
{
    public static class ApplicationRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<StockMovementValidator>();
            services.AddValidatorsFromAssemblyContaining<StoreValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            services.AddScoped<IToken, TokenService>();
            services.AddScoped<ILogin, LoginService>();
            services.AddScoped<IRegister, RegisterService>();
            services.AddScoped<IRefresh, RefreshService>();
            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStoreService, StoreService>();
            // services.AddMediatR(static cfg =>
            // {
            //     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // });

            services.AddScoped<IStockMovementService, StockMovementService>();

        }

    }
}