using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructures.FluentAPIs
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Combos)
                .WithOne(x => x.Shop)
                .HasForeignKey(x => x.ShopId);
            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Shop)
                .HasForeignKey(x => x.ShopId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
