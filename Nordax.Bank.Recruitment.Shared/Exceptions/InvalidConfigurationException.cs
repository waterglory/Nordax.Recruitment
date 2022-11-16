using System;

namespace Nordax.Bank.Recruitment.Shared.Exceptions
{
	public class InvalidConfigurationException : Exception 
	{
		public InvalidConfigurationException(string message) : base(message) { }
	}
}
