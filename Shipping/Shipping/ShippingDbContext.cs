using Microsoft.EntityFrameworkCore;

namespace Shipping
{
    public class ShippingDbContext : DbContext
    {
        public DbSet<ShippingLabel> ShippingLabels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost,14334;database=Monolith;user id=sa;password='qwert1&vleAw'");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShippingLabel>()
                .ToTable("ShippingLabels")
                .HasKey(x => x.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
