using Nordax.Bank.Recruitment.Shared.Models;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Providers
{
    public interface ICustomerProvider
    {
        public Task MergeCustomer(CustomerModel model);
        public Task<CustomerModel> GetCustomer(string organizationNo);
    }
}
