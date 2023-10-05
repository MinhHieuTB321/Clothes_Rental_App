using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Application.IRepositories;
using OrderService.Application;
using OrderService.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Application.Services;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commons;

namespace OrderService.Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppConfiguration appConfig)
        {

            #region DI_REPOSITORIES
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            #endregion

            #region DI_SERVICES
            services.AddScoped<ICurrentTime, CurrentTime>();
            services.AddScoped<IOrderService, Order_Service>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            #endregion
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(appConfig.DatabaseConnection));
            return services;
        }
    }
}
