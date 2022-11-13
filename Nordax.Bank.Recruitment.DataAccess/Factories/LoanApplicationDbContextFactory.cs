using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
	public interface ILoanApplicationDbContextFactory
	{
		LoanApplicationDbContext Create();
	}

	public class LoanApplicationDbContextFactory : ILoanApplicationDbContextFactory
	{
		private readonly LoanApplicationDbContext _dbContext;

		public LoanApplicationDbContextFactory(LoanApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public LoanApplicationDbContext Create()
		{
			return _dbContext;
		}
	}
}
