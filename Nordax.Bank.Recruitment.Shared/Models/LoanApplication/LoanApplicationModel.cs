using System;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Shared.Models.LoanApplication
{
	public class LoanApplicationModel
	{
		public Guid Id { get; set; }

		public string CaseNo { get; set; }

		public string CurrentStep { get; set; }

		public DateTime CreatedDate { get; set; }

		public ApplicantModel Applicant { get; set; }

		public LoanModel Loan { get; set; }

		public List<DocumentModel> Documents { get; set; }
	}
}
