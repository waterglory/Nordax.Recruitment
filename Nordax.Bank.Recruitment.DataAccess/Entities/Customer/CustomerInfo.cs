using Nordax.Bank.Recruitment.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.Customer
{
	public class CustomerInfo
	{
		public CustomerInfo() { }

		public CustomerInfo(CustomerModel model)
		{
			OrganizationNo = model.OrganizationNo;
			FromDomainModel(model);
		}

		[Key][MaxLength(12)] public string OrganizationNo { get; set; }

		[Required][MaxLength(100)] public string FirstName { get; set; }

		[Required][MaxLength(100)] public string Surname { get; set; }

		[Required][MaxLength(20)] public string PhoneNo { get; set; }

		[Required][MaxLength(200)] public string Email { get; set; }

		[Required][MaxLength(200)] public string Address { get; set; }

		[Required][MaxLength(20)] public string IncomeLevel { get; set; }

		public bool IsPoliticallyExposed { get; set; }

		public CustomerModel ToDomainModel() =>
			new()
			{
				OrganizationNo = OrganizationNo,
				FirstName = FirstName,
				Surname = Surname,
				PhoneNo = PhoneNo,
				Email = Email,
				Address = Address,
				IncomeLevel = IncomeLevel,
				IsPoliticallyExposed = IsPoliticallyExposed
			};

		public void FromDomainModel(CustomerModel model)
		{
			FirstName = model.FirstName;
			Surname = model.Surname;
			PhoneNo = model.PhoneNo;
			Email = model.Email;
			Address = model.Address;
			IncomeLevel = model.IncomeLevel;
		}
	}
}