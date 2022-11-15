using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;

namespace Nordax.Bank.Recruitment.Domain.Validators
{
    public interface ILoanApplicationValidator
    {
        bool Validate(LoanApplicationModel model, out string reason);
    }
}
