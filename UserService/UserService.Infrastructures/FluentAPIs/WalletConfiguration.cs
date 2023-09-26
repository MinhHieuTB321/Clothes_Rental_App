using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.FluentAPIs
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasOne(x=>x.User)
                   .WithMany(x=>x.Wallets)
                   .HasForeignKey(x=>x.UserId);

            builder.HasMany(x=>x.Transactions)
                   .WithOne(x=>x.Wallet)
                   .HasForeignKey(x=>x.WalletId);     
        }
    }
}