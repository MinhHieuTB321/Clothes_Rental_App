using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures.FluentAPIs
{
    public class ProductComboConfiguration
    {
        public void Configure(EntityTypeBuilder<ProductCombo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Combo)
                .WithMany(x => x.ProductCombos)
                .HasForeignKey(x => x.ComboId);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductCombos)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
