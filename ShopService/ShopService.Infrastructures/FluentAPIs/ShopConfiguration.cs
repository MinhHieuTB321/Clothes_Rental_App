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
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x=>x.ShopName).HasMaxLength(150);
            builder.HasIndex(x => x.ShopEmail).IsUnique();
            builder.HasIndex(x => x.ShopPhone).IsUnique();
            builder.HasOne(x => x.Owner).WithMany(x => x.Shop).HasForeignKey(x => x.OwnerId);
            builder.HasMany(x => x.Logo).WithOne(x => x.Shop).HasForeignKey(x => x.ShopId);
            builder.HasMany(x => x.Product).WithOne(x => x.Shop).HasForeignKey(x => x.ShopId);
        }
    }
}
