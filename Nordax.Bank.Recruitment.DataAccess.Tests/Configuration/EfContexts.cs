using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
	public class EfContexts<TDbContext> where TDbContext : DbContext
	{
		private readonly DbContextOptions<TDbContext> DbContextOptions =
			new DbContextOptionsBuilder<TDbContext>().UseInMemoryDatabase("InMemoryDb").Options;

		/// <summary>
		///     Use this context to create test data and verify already persisted data.
		/// </summary>
		/// <returns>ApplicationDbContext</returns>
		public TDbContext CreateInMemoryTestDbContext()
		{
			var dbContext = Activator.CreateInstance(typeof(TDbContext), DbContextOptions) as TDbContext;
			dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

			return dbContext;
		}

		/// <summary>
		///     Use this context to be passed on to a repository and/or used for application code.
		/// </summary>
		/// <returns>ApplicationDbContext</returns>
		public TDbContext CreateInMemoryDbContext() =>
			Activator.CreateInstance(typeof(TDbContext), DbContextOptions) as TDbContext;
	}
}
