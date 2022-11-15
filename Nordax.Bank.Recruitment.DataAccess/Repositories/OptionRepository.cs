using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
	public interface IOptionRepository
	{
		Task<List<BindingPeriodModel>> GetBindingPeriods();
	}

	public class OptionRepository : IOptionRepository
	{
		private readonly OptionDbContext _dbContext;

		public OptionRepository(IOptionDbContextFactory dbContextFactory)
		{
			_dbContext = dbContextFactory.Create();
		}

		public Task<List<BindingPeriodModel>> GetBindingPeriods() =>
			_dbContext.BindingPeriods.Select(bp => bp.ToDomainModel()).ToListAsync();
	}
}
