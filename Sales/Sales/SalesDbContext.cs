using Microsoft.EntityFrameworkCore;

namespace Sales
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost,14334;database=Monolith;user id=sa;password='qwert1&vleAw'");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
