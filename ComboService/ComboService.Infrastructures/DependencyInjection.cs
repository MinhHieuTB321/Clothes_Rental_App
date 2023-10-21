using ComboService.Application;
using ComboService.Application.AsyncDataServices;
using ComboService.Application.Commons;
using ComboService.Application.EventProcessing;
using ComboService.Application.Interfaces;
using ComboService.Application.Repositories;
using ComboService.Application.Services;
using ComboService.Infrastructures.AutoMapper;
using ComboService.Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComboService.Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, AppConfiguration appConfig)
        {

            #region DI_Service
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IComboService, CombosService>();
            services.AddScoped<IPriceListService, PriceListService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IProductService, ProductService>();
			#endregion

			services.AddHostedService<MessageBusSubcriber>();
			services.AddSingleton<IEventProcessor, EventProcessor>();
			services.AddSingleton<IMessageBusClient, MessageBusClient>();

			services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(appConfig.DatabaseConnection);
                //ptions.UseInMemoryDatabase("InMem");
            });


            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            return services;
        }
    }
}
