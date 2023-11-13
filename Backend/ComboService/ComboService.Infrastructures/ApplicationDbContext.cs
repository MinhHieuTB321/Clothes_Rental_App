using ComboService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Infrastructures
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        #region DbSet
        public DbSet<Combo> Combos { get; set; } = default!;
        public DbSet<PriceList> PriceLists { get; set; } = default!;
        public DbSet<ProductCombo> ProductCombos { get; set; } = default!;
        public DbSet<Shop> Shops { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
