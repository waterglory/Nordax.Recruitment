using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;

namespace Nordax.Bank.Recruitment.Domain.Validators
{
	public class AgeValidator : ILoanApplicationValidator
	{
		public bool Validate(LoanApplicationModel model, out string reason)
		{
			var organizationNo = model.Applicant.OrganizationNo;
			if (int.TryParse(organizationNo.Substring(0, 2), out var year))
			{
				var currentYear = DateTime.Now.Year;
				var previousCentury = ((currentYear / 100) - 1) * 100;
				var approximateAge = currentYear - year - previousCentury;
				if (approximateAge > 100) approximateAge -= 100;
				if (approximateAge < 22)
				{
					reason = "You're not in the appropriate age group to be elligible for this loan.";
					return false;
				}
				reason = null;
				return true;
			}
			else
			{
				reason = "Invalid year is specified in the organization number.";
				return false;
			}
		}
	}
}
