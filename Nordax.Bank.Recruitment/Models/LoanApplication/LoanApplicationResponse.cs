using System;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Models.LoanApplication
{
	public class LoanApplicationDocumentResponse
	{
		public Guid Id { get; set; }

		public string DocumentType { get; set; }
	}

	public class LoanApplicationResponse
	{
		public Guid Id { get; set; }

		public string CaseNo { get; set; }

		public string CurrentStep { get; set; }

		public DateTime CreatedDate { get; set; }

		public string ApplicantOrganizationNo { get; set; }

		public string ApplicantFullName { get; set; }

		public decimal LoanAmount { get; set; }

		public int LoanPaymentPeriod { get; set; }

		public int LoanBindingPeriod { get; set; }

		public decimal LoanInterestRate { get; set; }

		public List<LoanApplicationDocumentResponse> Documents { get; set; }
	}
}
