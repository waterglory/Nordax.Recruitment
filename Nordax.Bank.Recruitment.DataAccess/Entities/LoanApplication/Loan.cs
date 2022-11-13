using Microsoft.EntityFrameworkCore;
using System;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	public class Loan
	{
		public Guid Id { get; set; }

		[Precision(14, 2)] public decimal Amount { get; set; }

		[Precision(3)] public int BindingPeriod { get; set; }

		[Precision(8, 5)] public decimal InterestRate { get; set; }
	}
}
