using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	public class Document
	{
		public Document() { }

		public Document(DocumentModel model)
		{
			Id = model.Id;
			FromDomainModel(model);
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Required][MaxLength(20)] public string DocumentType { get; set; }

		[Required][MaxLength(50)] public string FileRef { get; set; }

		public DocumentModel ToDomainModel() =>
			new()
			{
				Id = Id,
				DocumentType = DocumentType,
				FileRef = FileRef,
			};

		public void FromDomainModel(DocumentModel model)
		{
			DocumentType = model.DocumentType;
			FileRef = model.FileRef;
		}
	}
}
