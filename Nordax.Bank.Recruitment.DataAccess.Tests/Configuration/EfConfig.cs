using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
	public static class EfConfig
	{
		public static EfContexts<FileDbContext> File = new EfContexts<FileDbContext>();
		public static EfContexts<OptionDbContext> Option = new EfContexts<OptionDbContext>();
		public static EfContexts<SubscriptionDbContext> Subscription = new EfContexts<SubscriptionDbContext>();
		public static EfContexts<LoanApplicationDbContext> LoanApplication = new EfContexts<LoanApplicationDbContext>();
	}
}
