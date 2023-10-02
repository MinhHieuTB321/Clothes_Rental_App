using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.FluentAPIs
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(o => o.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();
            builder.HasMany(u => u.Shops).WithOne(o=>o.Owner).HasForeignKey(x=>x.OwnerId);
        }
    }
}
