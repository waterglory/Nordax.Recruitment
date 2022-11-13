using System;
using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.Customer
{
	public class CustomerInfo
	{
		[Key][MaxLength(12)] public string OrganizationNo { get; set; }

		[Required][MaxLength(100)] public string FirstName { get; set; }

		[Required][MaxLength(100)] public string Surname { get; set; }

		[Required][MaxLength(20)] public string PhoneNo { get; set; }

		[Required][MaxLength(200)] public string Email { get; set; }

		[Required][MaxLength(200)] public string Address { get; set; }

		[Required][MaxLength(20)] public string IncomeLevel { get; set; }

		public bool IsPoliticallyExposed { get; set; }
	}
}
