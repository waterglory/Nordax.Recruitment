﻿using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;

namespace Nordax.Bank.Recruitment.Domain.Validators
{
	public class OrganizationNoValidator : ILoanApplicationValidator
	{
		public bool Validate(LoanApplicationModel model, out string reason)
		{
			reason = null;
			return true;
		}
	}
}