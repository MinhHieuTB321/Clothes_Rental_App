using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.FluentAPIs
{
    public class ShopLogoConfiguration : IEntityTypeConfiguration<ShopLogo>
    {
        public void Configure(EntityTypeBuilder<ShopLogo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Shop).WithMany(p => p.Logo).HasForeignKey(x => x.ShopId);
        }
    }
}
