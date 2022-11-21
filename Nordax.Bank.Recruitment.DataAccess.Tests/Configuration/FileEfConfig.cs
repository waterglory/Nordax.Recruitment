using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
	public static class FileEfConfig
	{
		private static readonly DbContextOptions<FileDbContext> FileDbContextOptions =
			new DbContextOptionsBuilder<FileDbContext>().UseInMemoryDatabase("InMemoryDb").Options;

		/// <summary>
		///     Use this context to create test data and verify already persisted data.
		/// </summary>
		/// <returns>ApplicationDbContext</returns>
		public static FileDbContext CreateInMemoryTestDbContext()
		{
			var dbContext = new FileDbContext(FileDbContextOptions);
			dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

			return dbContext;
		}

		/// <summary>
		///     Use this context to be passed on to a repository and/or used for application code.
		/// </summary>
		/// <returns>ApplicationDbContext</returns>
		public static FileDbContext CreateInMemoryFileDbContext()
		{
			return new(FileDbContextOptions);
		}
	}
}
