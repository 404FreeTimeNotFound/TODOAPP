using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOAPP.Data.Services;
using TODOAPP.Domain.Data;
using TODOAPP.Domain.Interfaces.IRepositories;
using TODOAPP.Domain.Interfaces.IServices;
using TODOAPP.Repositories;
using TODOAPP.Services;

namespace TODOAPP.ExtentionMethods
{
    public static class ServiceCollectionExtentions
    {
        public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IJwtGenerator,JwtGenerator>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
            return Task.FromResult(services);
        }
    }
}