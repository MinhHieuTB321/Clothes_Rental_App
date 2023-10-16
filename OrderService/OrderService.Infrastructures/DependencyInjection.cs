using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Application.IRepositories;
using OrderService.Application;
using OrderService.Infrastructures.Repositories;
using OrderService.Application.Services;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commons;
using OrderService.Application.AsyncDataServices;
using OrderService.Application.EventProcessing;

namespace OrderService.Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppConfiguration appConfig)
        {
            services.AddHostedService<MessageBusSubcriber>();
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            #region DI_REPOSITORIES
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();
            #endregion

            #region DI_SERVICES
            services.AddScoped<ICurrentTime, CurrentTime>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, Order_Service>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            #endregion
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(appConfig.DatabaseConnection));
            return services;
        }
    }
}
