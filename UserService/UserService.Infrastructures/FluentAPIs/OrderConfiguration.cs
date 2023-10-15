using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.FluentAPIs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id).ValueGeneratedNever();

            builder.HasOne(o=>o.Customer)
                .WithMany(u=>u.Orders)
                .HasForeignKey(o=>o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(o=>o.Payments)
                   .WithOne(p=>p.Order)
                   .HasForeignKey(p=>p.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}