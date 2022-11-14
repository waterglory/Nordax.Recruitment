using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
    public interface ISubscriptionDbContextFactory
    {
        SubscriptionDbContext Create();
    }

    public class SubscriptionDbContextFactory : DbContextFactory<SubscriptionDbContext>, ISubscriptionDbContextFactory
	{
		public SubscriptionDbContextFactory(SubscriptionDbContext dbContext) : base(dbContext) { }
	}
}