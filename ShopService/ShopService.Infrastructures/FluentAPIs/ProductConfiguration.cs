using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.FluentAPIs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductName).HasMaxLength(256);
            builder.Property(x => x.RootProduct).HasMaxLength(256);
            builder.Property(x => x.Size).HasMaxLength(10);
            builder.Property(x => x.Color).HasMaxLength(256);
            builder.Property(x => x.Material).HasMaxLength(256);
            builder.HasOne(x => x.Shop).WithMany(x => x.Product).HasForeignKey(x => x.ShopId);
            builder.HasOne(x => x.Category).WithMany(x => x.Product).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.RootProduct).WithMany().HasForeignKey(x => x.RootProductId);
            builder.HasMany(x=>x.ProductImages).WithOne(x=>x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
