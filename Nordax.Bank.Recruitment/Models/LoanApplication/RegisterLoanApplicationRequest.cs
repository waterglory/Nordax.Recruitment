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

		[Required] public string ApplicantAddress { get; set; }

		[Required] public string ApplicantIncomeLevel { get; set; }

		[Required] public bool ApplicantIsPoliticallyExposed { get; set; }

		[Required] public decimal LoanAmount { get; set; }

		[Required] public int LoanBindingPeriod { get; set; }

		[Required] public decimal LoanInterestRate { get; set; }

		[Required] public List<RegisterLoanApplicationDocumentRequest> Documents { get; set; }
	}
}
