using Microsoft.EntityFrameworkCore;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
	/// <summary>
	///     For InMemory DB to act like a relational database, there need to be seperate db contexts. One for the db logic and
	///     one to create test data and verify persisted data produced by the application db logic.
	///     As desribed here https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
	/// </summary>
	public static class EfConfig
    {
        private static readonly DbContextOptions<ApplicationDbContext> DbContextOptions =
            new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("InMemoryDb").Options;

        /// <summary>
        ///     Use this context to create test data and verify already persisted data.
        /// </summary>
        /// <returns>ApplicationDbContext</returns>
        public static ApplicationDbContext CreateInMemoryTestDbContext()
        {
            var dbContext = new ApplicationDbContext(DbContextOptions);
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return dbContext;
        }

        /// <summary>
        ///     Use this context to be passed on to a repository and/or used for application code.
        /// </summary>
        /// <returns>ApplicationDbContext</returns>
        public static ApplicationDbContext CreateInMemoryApplicationDbContext()
        {
            return new(DbContextOptions);
        }
    }
}