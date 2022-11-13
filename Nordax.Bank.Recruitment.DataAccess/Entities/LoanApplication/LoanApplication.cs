using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	[Index(nameof(CaseNo), IsUnique = true)]
	public class LoanApplication
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Required][MaxLength(20)] public string CaseNo { get; set; }

		[Required][MaxLength(20)] public string CurrentStep { get; set; }

		public DateTime CreatedDate { get; set; }

		[Required] public Applicant Applicant { get; set; }

		[Required] public Loan Loan { get; set; }

		[Required] public List<Document> Documents { get; set; }
	}
}
