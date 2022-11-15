using System;

namespace Nordax.Bank.Recruitment.Shared.Exceptions
{
	public class ValidationException : ApplicationException
	{
		public ValidationException(string reason) : base(reason) { }
	}
}
