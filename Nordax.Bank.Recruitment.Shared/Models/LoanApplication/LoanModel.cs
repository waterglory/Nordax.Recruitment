using System;

namespace Nordax.Bank.Recruitment.Shared.Models.LoanApplication
{
	public class LoanModel
	{
		public Guid Id { get; set; }

		public decimal Amount { get; set; }

		public int BindingPeriod { get; set; }

		public decimal InterestRate { get; set; }
	}
}
