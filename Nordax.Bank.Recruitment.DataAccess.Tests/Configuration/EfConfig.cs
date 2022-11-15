using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
    /// <summary>
    ///     For InMemory DB to act like a relational database, there need to be seperate db contexts. One for the db logic and
    ///     one to create test data and verify persisted data produced by the application db logic.
    ///     As desribed here https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
    /// </summary>
    public static class EfConfig
    {
        private static readonly DbContextOptions<SubscriptionDbContext> DbContextOptions =
            new DbContextOptionsBuilder<SubscriptionDbContext>().UseInMemoryDatabase("InMemoryDb").Options;

        /// <summary>
        ///     Use this context to create test data and verify already persisted data.
        /// </summary>
        /// <returns>ApplicationDbContext</returns>
        public static SubscriptionDbContext CreateInMemoryTestDbContext()
        {
            var dbContext = new SubscriptionDbContext(DbContextOptions);
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return dbContext;
        }

        /// <summary>
        ///     Use this context to be passed on to a repository and/or used for application code.
        /// </summary>
        /// <returns>ApplicationDbContext</returns>
        public static SubscriptionDbContext CreateInMemoryApplicationDbContext()
        {
            return new(DbContextOptions);
        }
    }
}