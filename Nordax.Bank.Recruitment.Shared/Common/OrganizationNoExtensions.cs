﻿using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Shared.Common
{
	public static class OrganizationNoExtensions
	{
		public static string CleanUpOrganizationNo(this string organizationNo) =>
			organizationNo.Replace("-", string.Empty);

		public static void CleanUpOrganizationNo(this LoanApplicationModel loanApplicationModel) =>
			loanApplicationModel.Applicant.OrganizationNo = loanApplicationModel.Applicant.OrganizationNo.CleanUpOrganizationNo();
	}
}
