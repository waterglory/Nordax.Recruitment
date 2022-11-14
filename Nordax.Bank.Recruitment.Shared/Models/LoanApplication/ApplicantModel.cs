using System;

namespace Nordax.Bank.Recruitment.Shared.Models.LoanApplication
{
	public class ApplicantModel
	{
		public Guid Id { get; set; }

		public string OrganizationNo { get; set; }

		public string FirstName { get; set; }

		public string Surname { get; set; }

		public string PhoneNo { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		public string IncomeLevel { get; set; }

		public bool IsPoliticallyExposed { get; set; }
	}
}
