using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.FluentAPIs
{
    public class OwnerConfiguration : IEntityTypeConfiguration<OwnerEntity>
    {
        public void Configure(EntityTypeBuilder<OwnerEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
            builder.HasIndex(o => o.Email).IsUnique();
            //builder.HasIndex(u => u.Phone).IsUnique();
            builder.HasMany(u => u.Shops).WithOne(o=>o.Owner).HasForeignKey(x=>x.Id);
        }
    }
}
