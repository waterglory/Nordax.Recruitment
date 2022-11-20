namespace Nordax.Bank.Recruitment.Models.LoanApplication
{
	public class GetCustomerDataResponse
	{
		public string FirstName { get; set; }

		public string Surname { get; set; }

		public string PhoneNo { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		public string IncomeLevel { get; set; }

		public bool IsPoliticallyExposed { get; set; }
	}
}
