using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	public class Loan
	{
		public Loan() { }

		public Loan(LoanModel model)
		{
			Id = model.Id;
			FromDomainModel(model);
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Precision(14, 2)] public decimal Amount { get; set; }

		[Precision(3)] public int BindingPeriod { get; set; }

		[Precision(8, 5)] public decimal InterestRate { get; set; }

		public LoanModel ToDomainModel() =>
			new()
			{
				Id = Id,
				Amount = Amount,
				BindingPeriod = BindingPeriod,
				InterestRate = InterestRate
			};

		public void FromDomainModel(LoanModel model)
		{
			Amount = model.Amount;
			BindingPeriod = model.BindingPeriod;
			InterestRate = model.InterestRate;
		}
	}
}
