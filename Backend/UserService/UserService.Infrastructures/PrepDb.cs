using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Infrastructures
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder, bool isProduction)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var appDb = serviceScope.ServiceProvider.GetService<AppDbContext>()!;
                SeedData(appDb!, isProduction);
            }
        }

        private static void SeedData(AppDbContext appDbContext, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attemping to apply migrations . . .");
                try
                {
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--->Could not run migration:{ex.Message}");
                }
            }
        }

        private static void AddUser(AppDbContext context)
        {
            if(!context.Users.Any())
            {
                Console.WriteLine("Seeding new users . . .");
                context.Users.AddRange(
                    new User { Id = Guid.Parse("7A59FCCC-A333-4A28-B403-7449202BFDBA"), Name = "User", Email = "User01@gmail.com", Phone = "0788972699", Role = "Customer", CreationDate = DateTime.Now, IsDeleted = false },
                    new User { Id = Guid.Parse("96f37f6a-f44b-40a1-8f07-6251d10171f7"), Name = "Admin", Email = "admin@gmail.com", Phone = "0915873762", Role = "Admin", CreationDate = DateTime.Now, IsDeleted = false },
                    new User { Id = Guid.Parse("07a5900a-99e2-49d3-a4dc-dab690b5a757"), Name = "Owner", Email = "onwer@gmail.com", Phone = "0286889721", Role = "Owner", CreationDate = DateTime.Now, IsDeleted = false }
                    );
            }
            else
            {
                Console.WriteLine("->> We already have users data!");
            }
        }

        private static void AddWallet(AppDbContext context)
        {
            if (!context.Wallets.Any())
            {
                Console.WriteLine("Seeding new wallets . . .");
                context.Wallets.AddRange(
                    new Wallet { Id = Guid.Parse("1e89b9d6-ce93-41bf-a973-7b81610466f5"), Balance = 999999999999999, UserId = Guid.Parse("7A59FCCC-A333-4A28-B403-7449202BFDBA"), CreationDate = DateTime.Now, IsDeleted = false },
                    new Wallet { Id = Guid.Parse("518830dd-06e3-4f43-adb7-4266f398ca50"), Balance = 999999999999999, UserId = Guid.Parse("96f37f6a-f44b-40a1-8f07-6251d10171f7"), CreationDate = DateTime.Now, IsDeleted = false },
                    new Wallet { Id = Guid.Parse("3705c1c2-ac2b-4f8d-9a49-9e0d915b5c1f"), Balance = 999999999999999, UserId = Guid.Parse("07a5900a-99e2-49d3-a4dc-dab690b5a757"), CreationDate = DateTime.Now, IsDeleted = false }
                    );
            }
            else
            {
                Console.WriteLine("->> We already have wallets data!");
            }
        }
    }
}
