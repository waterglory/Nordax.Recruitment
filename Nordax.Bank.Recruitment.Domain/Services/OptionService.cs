using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Services
{
	public interface IOptionService
	{
		Task<List<BindingPeriodModel>> GetBindingPeriods();
	}

	public class OptionService : IOptionService
	{
		private IOptionRepository _optionRepository;

		public OptionService(IOptionRepository optionRepository) =>
			_optionRepository = optionRepository;

		public Task<List<BindingPeriodModel>> GetBindingPeriods() =>
			_optionRepository.GetBindingPeriods();
	}
}
