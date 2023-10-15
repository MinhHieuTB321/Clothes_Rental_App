using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructures
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData(AppDbContext? appDbContext, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attemping to apply migrations . . .");
                try
                {
                    appDbContext!.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--->Could not run migration:{ex.Message}");
                }
                AddCombo(appDbContext);
                AddShop(appDbContext);
                AddCustomer(appDbContext);
            }
        }

        private static void AddShop(AppDbContext? appDbContext)
        {
            if(!appDbContext!.Shops.Any())
            {
                Console.WriteLine("->> Adding Shop");
                appDbContext!.Shops.Add(new Shop {
                    Id=Guid.Parse("f2a23750-3545-49f0-997b-ac16b986365b"),
                    ShopName= "Shop -1",
                    ShopCode= "24711334455",
                    ShopEmail= "shop01@gmail.com",
                    ShopPhone= "0938728080",
                    Address= "Shop - Address-1",
                    Status= "Active",
                    OwnerId=Guid.Parse("07a5900a-99e2-49d3-a4dc-dab690b5a757"),
                    CreationDate=DateTime.Now,
                    IsDeleted=false
                });
            }
            else
            {
                Console.WriteLine("->> We already have shop data!");
            }
        }


        private static void AddCombo(AppDbContext? appDbContext)
        {
            if (!appDbContext!.Combos.Any())
            {
                Console.WriteLine("->> Adding Combos");
                appDbContext!.Combos.AddRange(
                    new Combo {Id=Guid.Parse("2d4820c3-3f0f-4fdd-a9a1-0945e1a64506"),ComboName="Combo - 1",Quantity=3,Status="Active",CreationDate=DateTime.Now,IsDeleted=false,ShopId=Guid.Parse("f2a23750-3545-49f0-997b-ac16b986365b") },
                    new Combo { Id = Guid.Parse("2f2e9033-cd20-4863-bf71-86c78b5fc922"), ComboName = "Combo - 2", Quantity = 5, Status = "Active", CreationDate = DateTime.Now, IsDeleted = false, ShopId = Guid.Parse("f2a23750-3545-49f0-997b-ac16b986365b") },
                    new Combo { Id = Guid.Parse("247f4c12-e782-456c-aedd-1afb97b30c53"), ComboName = "Combo - 3", Quantity = 10, Status = "Active", CreationDate = DateTime.Now, IsDeleted = false, ShopId = Guid.Parse("f2a23750-3545-49f0-997b-ac16b986365b") }
                    );
            }
            else
            {
                Console.WriteLine("->> We already have Combos data!");
            }
        }

        private static void AddCustomer(AppDbContext? appDbContext)
        {
            if (!appDbContext!.Customers.Any())
            {
                Console.WriteLine("->> Adding Customer");
                appDbContext!.Customers.Add(new Customer
                {
                    Id = Guid.Parse("7A59FCCC-A333-4A28-B403-7449202BFDBA"),
                    Name = "User - 1",
                    Gender = "Male",
                    Email = "User01@gmail.com",
                    Phone = "0788972699",
                    Address = "User - Address - 1",
                    Status = "Active",
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                });
            }
            else
            {
                Console.WriteLine("->> We already have customers data!");
            }
        }
    }
}
