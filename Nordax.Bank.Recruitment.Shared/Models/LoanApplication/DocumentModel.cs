using System;

namespace Nordax.Bank.Recruitment.Shared.Models.LoanApplication
{
	public class DocumentModel
	{
		public Guid Id { get; set; }

		public string DocumentType { get; set; }

		public string FileRef { get; set; }
	}
}
