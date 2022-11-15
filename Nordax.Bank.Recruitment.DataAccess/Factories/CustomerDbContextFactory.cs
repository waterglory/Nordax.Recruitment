using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
	public interface ICustomerDbContextFactory
	{
		CustomerDbContext Create();
	}

	public class CustomerDbContextFactory : DbContextFactory<CustomerDbContext>, ICustomerDbContextFactory
	{
		public CustomerDbContextFactory(CustomerDbContext dbContext) : base(dbContext) { }
	}
}
