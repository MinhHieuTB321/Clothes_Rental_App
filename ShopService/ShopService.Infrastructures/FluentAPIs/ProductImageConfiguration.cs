using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.FluentAPIs
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImageEntity>
    {
        public void Configure(EntityTypeBuilder<ProductImageEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            //builder.Property(x => x.ProductImageUrl).HasMaxLength();
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue("False");
        }
    }
}
