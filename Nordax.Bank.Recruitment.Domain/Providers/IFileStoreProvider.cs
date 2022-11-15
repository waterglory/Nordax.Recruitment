using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Providers
{
	public interface IFileStoreProvider
	{
		Task<FileModel> GetFile(string fileRef);
		Task<string> SaveFile(string documentType, string fileFullName, byte[] content);
	}
}
