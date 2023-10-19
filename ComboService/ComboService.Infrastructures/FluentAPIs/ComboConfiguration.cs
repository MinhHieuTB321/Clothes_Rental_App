using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures.FluentAPIs
{
    public class ComboConfiguration : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Shop)
                .WithMany(s => s.Combos)
                .HasForeignKey(x => x.ShopId);

            builder.HasMany(x => x.PriceLists).WithOne(x => x.Combo);
            builder.HasMany(x => x.ProductCombos).WithOne(x => x.Combo);
        }
    }
}
