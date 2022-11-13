using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	public class Document
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Required][MaxLength(20)] public string DocumentType { get; set; }

		[Required][MaxLength(50)] public string FileRef { get; set; }
	}
}
