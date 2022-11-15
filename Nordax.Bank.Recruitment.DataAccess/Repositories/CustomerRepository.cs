using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.Customer;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.Shared.Models;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
	public interface ICustomerRepository
	{
		Task MergeCustomer(CustomerModel model);
		Task<CustomerModel> GetCustomer(string organizationNo);
	}

	public class CustomerRepository : ICustomerRepository
	{
		private readonly CustomerDbContext _dbContext;

		public CustomerRepository(ICustomerDbContextFactory dbContextFactory)
		{
			_dbContext = dbContextFactory.Create();
		}

		public async Task MergeCustomer(CustomerModel model)
		{
			var existingCustomer = await _dbContext.CustomerInfos.FirstOrDefaultAsync(c => c.OrganizationNo == model.OrganizationNo);
			if (existingCustomer == null)
				_dbContext.Add(new CustomerInfo(model));
			else
				existingCustomer.FromDomainModel(model);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<CustomerModel> GetCustomer(string organizationNo) =>
			(await _dbContext.CustomerInfos
				.FirstOrDefaultAsync(c => c.OrganizationNo == organizationNo))?
				.ToDomainModel();
	}
}
