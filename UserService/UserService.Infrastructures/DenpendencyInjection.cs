using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application;
using UserService.Application.Interfaces;
using UserService.Application.IRepositories;
using UserService.Application.Services;
using UserService.Infrastructures.Repositories;

namespace UserService.Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {

            #region DI_REPOSITORIES
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<IWalletRepository,WalletRepository>();
            services.AddScoped<ITransactionRepository,TransactionRepository>();
            services.AddScoped<IPaymentRepository,PaymentRepository>();
            #endregion

            #region DI_SERVICES
            services.AddScoped<ICurrentTime,CurrentTime>();
            services.AddScoped<IMemberService,MemberService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IPaymentService,PaymentService>();
            services.AddScoped<IWalletService,WalletService>();
            services.AddScoped<ITransactionService,TransactionService>();
            services.AddScoped<IAuthenticationService,AuthenticationService>();
            #endregion
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(databaseConnection));  
            return services;
        } 
    }
}