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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Combo)
               .WithMany(x => x.OrderDetails)
               .HasForeignKey(x => x.ComboId);

            builder.HasOne(x => x.Shop)
               .WithMany(x => x.OrderDetails)
               .HasForeignKey(x => x.ShopId);

            builder.HasOne(x => x.Fee)
               .WithMany(x => x.OrderDetails)
               .HasForeignKey(x => x.FeeId);
        }
    }
}
