using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructures
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Order> Orders{get;set;}=default!;
        public DbSet<User> Users{get;set;}=default!;
        public DbSet<Payment> Payments{get;set;}=default!;
        public DbSet<Transaction> Transactions{get;set;}=default!;
        public DbSet<Wallet> Wallets{get;set;}=default!;
    }
}