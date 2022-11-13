using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities.Subscription;

namespace Nordax.Bank.Recruitment.DataAccess.DbContexts
{
    public class SubscriptionDbContext : DbContext
    {
        public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options) : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subscription>().HasIndex(p => p.Email).IsUnique();
        }
    }
}