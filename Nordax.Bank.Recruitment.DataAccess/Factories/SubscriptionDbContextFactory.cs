using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
    public interface ISubscriptionDbContextFactory
    {
        SubscriptionDbContext Create();
    }

    public class SubscriptionDbContextFactory : ISubscriptionDbContextFactory
    {
        private readonly SubscriptionDbContext _dbContext;

        public SubscriptionDbContextFactory(SubscriptionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SubscriptionDbContext Create()
        {
            return _dbContext;
        }
    }
}