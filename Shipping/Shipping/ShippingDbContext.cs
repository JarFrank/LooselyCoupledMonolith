using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Shipping
{
    public class ShippingDbContext : DbContext
    {
        public DbSet<ShippingLabel> ShippingLabels { get; set; }
        public DbSet<IdempotentConsumer> DuplicateMessages { get; set; }

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

            modelBuilder.Entity<IdempotentConsumer>()
                .ToTable("IdempotentConsumers")
                .HasKey(x => new { x.MessageId, x.Consumer });
            base.OnModelCreating(modelBuilder);
        }

        public async Task IdempotentConsumer(long messageId, string consumer)
        {
            await DuplicateMessages.AddAsync(new IdempotentConsumer
            {
                MessageId = messageId,
                Consumer = consumer,
            });
            await SaveChangesAsync();
        }

        public async Task<bool> HasBeenProcessed(long messageId, string consumer)
        {
            return await DuplicateMessages.AnyAsync(x => x.Consumer == consumer && x.MessageId == messageId);
        }
    }
}
