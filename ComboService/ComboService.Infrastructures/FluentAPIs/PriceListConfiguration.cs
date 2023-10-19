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
    public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Combo).WithMany(c => c.PriceLists)
                .HasForeignKey(x => x.ComboId);
        }
    }
}
