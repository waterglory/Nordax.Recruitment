using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
	public abstract class DbContextFactory<TDbContext> where TDbContext : DbContext
	{
		protected readonly TDbContext _dbContext;

		public DbContextFactory(TDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public TDbContext Create()
		{
			return _dbContext;
		}
	}
}
