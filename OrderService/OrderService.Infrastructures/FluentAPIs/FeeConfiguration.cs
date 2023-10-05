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
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
           builder.HasKey(x => x.Id);
            builder.HasMany(x=>x.OrderDetails)
                .WithOne(x => x.Fee)
                .HasForeignKey(x => x.FeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x=>x.Combo)
                .WithMany(x=>x.Fees)
                .HasForeignKey(x => x.ComboId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
