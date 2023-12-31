﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Application.Interfaces;
using ShopService.Application.IRepositories;
using ShopService.Application.Services;
using ShopService.Application;
using ShopService.Infrastructures.Repositories;
using System.Text.Json.Serialization;
using ShopService.Application.EventProcessing;
using ShopService.Application.AsyncDataServices;

namespace ShopService.Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
        {

            services.AddScoped<IMessageBusClient, MessageBusClient>();
            services.AddScoped<IEventProcessor, EventProcessor>();
            services.AddHostedService<MessageBusSubcriber>();
            #region DI_REPOSITORIES
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region DI_SERVICES
            services.AddScoped<ICurrentTime, CurrentTime>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopService, ShopinService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            #endregion

            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connectionString)
                );
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
