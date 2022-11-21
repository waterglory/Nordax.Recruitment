using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
	public static class OptionEfConfig
	{
		private static readonly DbContextOptions<OptionDbContext> OptionDbContextOptions =
			new DbContextOptionsBuilder<OptionDbContext>().UseInMemoryDatabase("InMemoryDb").Options;

		/// <summary>
		///     Use this context to create test data and verify already persisted data.
		/// </summary>
		/// <returns>ApplicationDbContext</returns>
		public static OptionDbContext CreateInMemoryTestDbContext()
		{
			var dbContext = new OptionDbContext(OptionDbContextOptions);
			dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

			return dbContext;
		}

		/// <summary>
		///     Use this context to be passed on to a repository and/or used for application code.
		/// </summary>
		/// <returns>ApplicationDbContext</returns>
		public static OptionDbContext CreateInMemoryOptionDbContext()
		{
			return new(OptionDbContextOptions);
		}
	}
}
