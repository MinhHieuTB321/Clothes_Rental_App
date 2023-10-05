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
    public class ComboConfiguration : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Shop)
                .WithMany(x => x.Combos)
                .HasForeignKey(x => x.ShopId);

            builder.HasMany(x=>x.Fees)
                .WithOne(x=>x.Combo)
                .HasForeignKey(x=>x.ComboId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Combo)
                .HasForeignKey(x => x.ComboId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
