using Microsoft.EntityFrameworkCore;
using ShopService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        #region DbSet
        public DbSet<ShopEntity> Shop { get; set; }
        public DbSet<OwnerEntity> Owner { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<ProductEnity> Product { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
