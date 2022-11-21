using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.Models.LoanApplication
{
	public class RegisterLoanApplicationDocumentRequest
	{
		[Required] public string DocumentType { get; set; }

		[Required] public string FileRef { get; set; }
	}

	public class RegisterLoanApplicationRequest
	{
		[Required] public string ApplicantOrganizationNo { get; set; }

		[Required] public string ApplicantFirstName { get; set; }

		[Required] public string ApplicantSurname { get; set; }

		[Required] public string ApplicantPhoneNo { get; set; }

		[Required] public string ApplicantEmail { get; set; }

		public string ApplicantAddress { get; set; }

		public string ApplicantIncomeLevel { get; set; }

		public bool ApplicantIsPoliticallyExposed { get; set; }

		public decimal LoanAmount { get; set; }

		public int LoanPaymentPeriod { get; set; }

		public int LoanBindingPeriod { get; set; }

		public decimal LoanInterestRate { get; set; }

		[Required] public List<RegisterLoanApplicationDocumentRequest> Documents { get; set; }
	}
}
