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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.RootProduct).WithMany()
                .HasForeignKey(x => x.RootProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasMany(x => x.ChildProducts).WithOne(x => x.RootProduct)
                .HasForeignKey(x => x.RootProductId);
            builder.HasMany(x => x.ProductCombos).WithOne(x => x.Product)
                .IsRequired(false);
        }
    }
}
