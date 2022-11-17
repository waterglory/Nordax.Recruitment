using System;

namespace Nordax.Bank.Recruitment.Shared.Exceptions
{
	/// <summary>
	/// An exception for customer that already has ongoing application and tries to submit a new loan application.
	/// The message of this exception contains the ongoing application's case number.
	/// </summary>
	public class CustomerOngoingLoanApplicationException : Exception
	{
		public CustomerOngoingLoanApplicationException(string caseNo) : base(caseNo) { }
	}
}
