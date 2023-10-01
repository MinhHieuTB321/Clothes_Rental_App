using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.FluentAPIs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedNever();
            
            builder.HasMany(u=>u.Orders)
                .WithOne(o=>o.Payer)
                .HasForeignKey(o=>o.PayerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(u=>u.Wallets)
                .WithOne(w=>w.User)
                .HasForeignKey(w=>w.UserId);
            
            builder.HasMany(u=>u.Payments)
                .WithOne(p=>p.User)
                .HasForeignKey(p=>p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}