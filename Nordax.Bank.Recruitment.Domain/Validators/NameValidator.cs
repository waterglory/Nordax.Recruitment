using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;

namespace Nordax.Bank.Recruitment.Domain.Validators
{
	public class NameValidator : ILoanApplicationValidator
	{
		public bool Validate(LoanApplicationModel model, out string reason)
		{
			reason = null;
			return true;
		}
	}
}
