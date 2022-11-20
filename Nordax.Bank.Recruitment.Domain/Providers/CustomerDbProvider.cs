using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Shared.Common;
using Nordax.Bank.Recruitment.Shared.Models;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Providers
{
	public class CustomerDbProvider : ICustomerProvider
	{
		private ICustomerRepository _customerRepository;

		public CustomerDbProvider(ICustomerRepository customerRepository) =>
			_customerRepository = customerRepository;

		public Task<CustomerModel> GetCustomer(string organizationNo) =>
			_customerRepository.GetCustomer(organizationNo.CleanUpOrganizationNo());

		public Task MergeCustomer(CustomerModel model)
		{
			model.OrganizationNo = model.OrganizationNo.CleanUpOrganizationNo();
			return _customerRepository.MergeCustomer(model);
		}
	}
}
