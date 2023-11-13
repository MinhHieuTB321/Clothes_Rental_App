using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.FluentAPIs
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasOne(p=>p.Order)
                   .WithMany(o=>o.Payments)
                   .HasForeignKey(p=>p.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(p=>p.Customer)
                   .WithMany(u=>u.Payments)
                   .HasForeignKey(p=>p.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(p=>p.Transactions)
                   .WithOne(t=>t.Payment)
                   .HasForeignKey(t=>t.PaymentId);
        }
    }
}