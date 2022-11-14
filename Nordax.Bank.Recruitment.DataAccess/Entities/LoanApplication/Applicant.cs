using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication
{
	public class Applicant
	{
		public Applicant() { }

		public Applicant(ApplicantModel model)
		{
			Id = model.Id;
			FromDomainModel(model);
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Required][MaxLength(12)] public string OrganizationNo { get; set; }

		[Required][MaxLength(100)] public string FirstName { get; set; }

		[Required][MaxLength(100)] public string Surname { get; set; }

		[Required][MaxLength(20)] public string PhoneNo { get; set; }

		[Required][MaxLength(200)] public string Email { get; set; }

		[Required][MaxLength(200)] public string Address { get; set; }

		[Required][MaxLength(20)] public string IncomeLevel { get; set; }

		public bool IsPoliticallyExposed { get; set; }

		public ApplicantModel ToDomainModel() =>
			new()
			{
				Id = Id,
				OrganizationNo = OrganizationNo,
				FirstName = FirstName,
				Surname = Surname,
				PhoneNo = PhoneNo,
				Email = Email,
				Address = Address,
				IncomeLevel = IncomeLevel,
				IsPoliticallyExposed = IsPoliticallyExposed
			};

		public void FromDomainModel(ApplicantModel model)
		{
			OrganizationNo = model.OrganizationNo;
			FirstName = model.FirstName;
			Surname = model.Surname;
			PhoneNo = model.PhoneNo;
			Email = model.Email;
			Address = model.Address;
			IncomeLevel = model.IncomeLevel;
			IsPoliticallyExposed = model.IsPoliticallyExposed;
		}
	}
}
