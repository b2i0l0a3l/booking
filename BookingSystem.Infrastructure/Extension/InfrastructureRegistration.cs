using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Interfaces;
using BookingSystem.Infrastructure.presistence.Repo;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApi.Infrastructure.InfrastructureRegistration
{
    public static class InfrastructureRegistration 
    {
        public static void AddInfrastructureRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReposatory<>), typeof(Reposatory<>));
        }
    }
}