using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
	public interface IOptionDbContextFactory
	{
		OptionDbContext Create();
	}

	public class OptionDbContextFactory : DbContextFactory<OptionDbContext>, IOptionDbContextFactory
	{
		public OptionDbContextFactory(OptionDbContext dbContext) : base(dbContext) { }
	}
}
