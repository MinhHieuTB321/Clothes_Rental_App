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
            builder.HasIndex(x => x.ShopEmail).IsUnique();
            builder.HasIndex(x => x.ShopPhone).IsUnique();
            builder.HasOne(x => x.Owner).WithMany(x => x.Shops).HasForeignKey(x => x.OwnerId);
            builder.HasMany(x => x.Products).WithOne(x => x.Shop).HasForeignKey(x => x.ShopId);
        }
    }
}
