using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
	public interface ILoanApplicationDbContextFactory
	{
		LoanApplicationDbContext Create();
	}

	public class LoanApplicationDbContextFactory : DbContextFactory<LoanApplicationDbContext>, ILoanApplicationDbContextFactory
	{
		public LoanApplicationDbContextFactory(LoanApplicationDbContext dbContext) : base(dbContext) { }
	}
}
