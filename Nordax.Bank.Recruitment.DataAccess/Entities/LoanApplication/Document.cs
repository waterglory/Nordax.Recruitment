using System;
using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	public class Document
	{
		public Guid Id { get; set; }

		[Required][MaxLength(20)] public string DocumentType { get; set; }

		[Required][MaxLength(50)] public string FileRef { get; set; }
	}
}
