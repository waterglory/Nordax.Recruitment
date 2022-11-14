using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	[Index(nameof(CaseNo), IsUnique = true)]
	public class LoanApplication
	{
		public LoanApplication() { }

		public LoanApplication(LoanApplicationModel model)
		{
			Id = model.Id;
			FromDomainModel(model);
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Required][MaxLength(20)] public string CaseNo { get; set; }

		[Required][MaxLength(20)] public string CurrentStep { get; set; }

		public DateTime CreatedDate { get; set; }

		[Required] public Applicant Applicant { get; set; }

		[Required] public Loan Loan { get; set; }

		[Required] public List<Document> Documents { get; set; }

		public LoanApplicationModel ToDomainModel() =>
			new()
			{
				Id = Id,
				CaseNo = CaseNo,
				CurrentStep = CurrentStep,
				CreatedDate = CreatedDate,
				Applicant = Applicant.ToDomainModel(),
				Loan = Loan.ToDomainModel(),
				Documents = Documents.Select(d => d.ToDomainModel()).ToList()
			};

		public void FromDomainModel(LoanApplicationModel model)
		{
			CaseNo = model.CaseNo;
			CurrentStep = model.CurrentStep;
			CreatedDate= model.CreatedDate;
			Applicant = new Applicant(model.Applicant);
			Loan = new Loan(model.Loan);
			Documents = model.Documents.Select(d => new Document(d)).ToList();
		}
	}
}
