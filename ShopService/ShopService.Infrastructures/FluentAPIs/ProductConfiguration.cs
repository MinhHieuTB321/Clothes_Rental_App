using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.FluentAPIs
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEnity>
    {
        public void Configure(EntityTypeBuilder<ProductEnity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.ProductName).HasMaxLength(256);
            builder.Property(x => x.ProductRoot).HasMaxLength(256);
            builder.Property(x => x.Size).HasMaxLength(10);
            builder.Property(x => x.Color).HasMaxLength(256);
            builder.Property(x => x.Material).HasMaxLength(256);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue("False");
            builder.HasOne(x => x.Shop).WithMany(x => x.Products).HasForeignKey(x => x.Id);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.Id);
            builder.HasOne(x => x.Product).WithMany(x => x.Products).HasForeignKey(x => x.Id);
            builder.HasMany(x=>x.ProductImages).WithOne(x=>x.Product).HasForeignKey(x => x.Id);
        }
    }
}
