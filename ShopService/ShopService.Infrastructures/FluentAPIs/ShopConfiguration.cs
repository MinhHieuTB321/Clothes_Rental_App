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
    public class ShopConfiguration : IEntityTypeConfiguration<ShopEntity>
    {
        public void Configure(EntityTypeBuilder<ShopEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x=>x.ShopName).HasMaxLength(150);
            builder.HasIndex(x => x.ShopEmail).IsUnique();
            builder.HasIndex(x => x.ShopPhone).IsUnique();
            builder.Property(x => x.IsDeleted).HasDefaultValue("False");
            builder.Property(x => x.IsActive).HasDefaultValue("False");
            builder.HasOne(x => x.Owner).WithMany(x => x.Shops).HasForeignKey(x => x.Id);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue("False");
        }
    }
}
