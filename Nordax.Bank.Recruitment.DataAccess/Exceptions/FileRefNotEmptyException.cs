using System;

namespace Nordax.Bank.Recruitment.DataAccess.Exceptions
{
	/// <summary>
	/// An exception for an already used file reference, when the saving operation is not forced.
	/// The message of this exception contains the conflicting file reference.
	/// </summary>
	public class FileRefNotEmptyException : Exception
	{
		public FileRefNotEmptyException(string fileRef) : base(fileRef) { }
	}
}
