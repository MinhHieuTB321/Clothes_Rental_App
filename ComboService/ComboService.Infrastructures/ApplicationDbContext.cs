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
        public DbSet<Combo> Combo { get; set; } = default!;
        public DbSet<PriceList> PriceList { get; set; } = default!;
        public DbSet<ProductCombo> ProductCombo { get; set; } = default!;
        public DbSet<Shop> Shop { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
