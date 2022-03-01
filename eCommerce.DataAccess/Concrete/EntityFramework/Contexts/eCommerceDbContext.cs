using eCommerce.Core.Utilities.Configuration;
using eCommerce.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eCommerce.DataAccess.Concrete.EntityFramework.Contexts
{
    public class eCommerceDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = ConfigurationHelper.GetConfig("dalsettings");
                var conStr = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(conStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
