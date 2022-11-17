using System;

namespace Nordax.Bank.Recruitment.DataAccess.Exceptions
{
	public class FileRefNotEmptyException : Exception
	{
		public FileRefNotEmptyException(string fileRef) : base(fileRef) { }
	}
}
