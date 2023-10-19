using ComboService.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>(), isProduction);
            }
        }

        private static void SeedData(ApplicationDbContext? context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attemping to apply migrations . . .");
                try
                {
                    context!.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--->Could not run migration:{ex.Message}");
                }

            }
            AddShop(context);
            AddProduct(context);
            AddCombo(context);
            AddPriceList(context);
        }

        private static void AddProduct(ApplicationDbContext contexet)
        {
            if (!contexet!.Product.Any())
            {
                Console.WriteLine("->> Adding Product");
                contexet!.Product.AddRange(
                new Product
                {
                    Id = Guid.Parse("74c46a37-8d6e-444b-817d-59caf1e61b9d"),
                    RootProductId = null,
                    Description = "Beautiful Dress",
                    ProductName = "Dress",
                    Size = "M",
                    Color = "White",
                    Material = "Cotton",
                    Price = 1600000,
                    Status = "Active",
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                },

                new Product
                {
                    Id = Guid.Parse("938e1e63-2757-4ab7-b61f-c55fb5eb906a"),
                    RootProductId = Guid.Parse("74c46a37-8d6e-444b-817d-59caf1e61b9d"),
                    Description = "Beautiful Dress",
                    Size = "M",
                    Color = "White",
                    Material = "Cotton",
                    Price = 1600000,
                    ProductName = "Pixie Dress",
                    Status = "Active",
                    Compensation = 1000000,
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    Id = Guid.Parse("a0cc3ed1-f9ea-4c5b-8625-1a4fc40d9f39"),
                    RootProductId = Guid.Parse("74c46a37-8d6e-444b-817d-59caf1e61b9d"),
                    Description = "Beautiful Dress",
                    Size = "M",
                    Color = "Black",
                    Material = "Cotton",
                    Price = 1100000,
                    ProductName = "Lana Dress",
                    Status = "Active",
                    Compensation = 850000,
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                },
                new Product
                {
                    Id = Guid.Parse("0ffebf97-0113-4af1-8530-aec602279683"),
                    RootProductId = Guid.Parse("74c46a37-8d6e-444b-817d-59caf1e61b9d"),
                    Description = "Beautiful Dress",
                    Size = "M",
                    Color = "Red",
                    Material = "Cotton",
                    Price = 950000,
                    Status = "Active",
                    ProductName = "Blood Mini Dress",
                    Compensation = 700000,
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                }
                );
                contexet.SaveChanges();

            }
            else
            {
                Console.WriteLine("->> We already have product data!");
            }
        }

        private static void AddShop(ApplicationDbContext contexet)
        {
            if (!contexet!.Shop.Any())
            {
                Console.WriteLine("->> Adding Shop");
                contexet!.Shop.Add(new Shop
                {
                    Id = Guid.Parse("f2a23750-3545-49f0-997b-ac16b986365b"),
                    ShopName = "Shop -1",
                    ShopCode = "24711334455",
                    ShopEmail = "shop01@gmail.com",
                    ShopPhone = "0938728080",
                    Address = "Shop - Address-1",
                    Status = "Active",
                    OwnerId = Guid.Parse("07a5900a-99e2-49d3-a4dc-dab690b5a757"),
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                });

                contexet.SaveChanges();
            }
            else
            {
                Console.WriteLine("->> We already have shop data!");
            }
        }

        private static void AddCombo(ApplicationDbContext context)
        {
            if (!context!.Combo.Any())
            {
                Console.WriteLine("->> Adding Combo");
                context!.Combo.Add(new Combo
                {
                    Id = Guid.Parse("4d0939d2-a00e-4e35-88de-212b4686b238"),
                    ComboName = "Combo 1",
                    Quantity = 5,
                    Status = "Active",
                    TotalValue = 7300000,
                    ShopId = Guid.Parse("f2a23750-3545-49f0-997b-ac16b986365b"),
                    ProductCombos = new List<ProductCombo>
                    {
                        new ProductCombo
                        {
                            Quantity = 2,
                            ComboId = Guid.Parse("4d0939d2-a00e-4e35-88de-212b4686b238"),
                            ProductId =  Guid.Parse("938e1e63-2757-4ab7-b61f-c55fb5eb906a"),
                        },
                        new ProductCombo
                        {
                            Quantity = 2,
                            ComboId = Guid.Parse("4d0939d2-a00e-4e35-88de-212b4686b238"),
                            ProductId =  Guid.Parse("a0cc3ed1-f9ea-4c5b-8625-1a4fc40d9f39"),
                        },
                        new ProductCombo
                        {
                            Quantity = 2,
                            ComboId = Guid.Parse("4d0939d2-a00e-4e35-88de-212b4686b238"),
                            ProductId =  Guid.Parse("0ffebf97-0113-4af1-8530-aec602279683"),
                        }
                    },
                    CreationDate = DateTime.Now,
                    IsDeleted = false

                });
                context.SaveChanges();
            }
        }
        private static void AddPriceList(ApplicationDbContext context)
        {
            if (!context!.PriceList.Any())
            {
                Console.WriteLine("->> Adding PriceList");
                context!.PriceList.AddRange(
                 new PriceList
                {
                    Deposit = 6300000,
                    RentalPrice = 800000,
                    Duration = "1 Week",
                    ComboId = Guid.Parse("4d0939d2-a00e-4e35-88de-212b4686b238"),
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                 },

                new PriceList
                {
                    Deposit = 6300000,
                    RentalPrice = 1400000,
                    Duration = "2 Week",
                    ComboId = Guid.Parse("4d0939d2-a00e-4e35-88de-212b4686b238"),
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                });

                context.SaveChanges();
            }

        }
    }
}
