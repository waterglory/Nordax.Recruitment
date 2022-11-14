using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Shared.Models
{
	public class CustomerModel
	{
		public string OrganizationNo { get; set; }

		public string FirstName { get; set; }

		public string Surname { get; set; }

		public string PhoneNo { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		public string IncomeLevel { get; set; }

		public bool IsPoliticallyExposed { get; set; }
	}
}
