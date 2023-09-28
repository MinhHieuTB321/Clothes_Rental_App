
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.FluentAPIs
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
           builder.HasKey(x=>x.Id);

           builder.HasOne(t=>t.Payment)
                  .WithMany(p=>p.Transactions)
                  .HasForeignKey(p=>p.PaymentId);
                  
            builder.HasOne(t=>t.Wallet)
                  .WithMany(p=>p.Transactions)
                  .HasForeignKey(p=>p.WalletId);
        }
    }
}