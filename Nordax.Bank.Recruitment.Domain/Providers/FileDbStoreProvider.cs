using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Providers
{
	public class FileDbStoreProvider : IFileStoreProvider
	{
		private IFileRepository _fileRepository;

		public FileDbStoreProvider(IFileRepository fileRepository) =>
			_fileRepository = fileRepository;

		private (string, string) ExtractFilenameAndExtension(string fileFullName)
		{
			var dotIdx = fileFullName.LastIndexOf('.');
			var fileName = fileFullName.Substring(0, dotIdx);
			var fileExt = fileFullName.Substring(dotIdx + 1);
			return (fileName, fileExt);
		}

		public Task<FileModel> GetFile(string fileRef) =>
			_fileRepository.GetFile(fileRef);

		public async Task<string> SaveFile(string documentType, string fileFullName, string contentType, byte[] content)
		{
			if (string.IsNullOrWhiteSpace(documentType))
				throw new ArgumentException(nameof(documentType));
			if (string.IsNullOrWhiteSpace(fileFullName))
				throw new ArgumentException(nameof(fileFullName));
			if (content == null)
				throw new ArgumentNullException(nameof(content));

			(var fileName, var fileExt) = ExtractFilenameAndExtension(fileFullName);
			var fileModel = new FileModel
			{
				FileRef = $"{documentType}/{Guid.NewGuid()}",
				FileName = fileName,
				ContentType = contentType,
				Content = content,
				FileType = fileExt
			};
			await _fileRepository.SaveFile(fileModel);

			return fileModel.FileRef;
		}
	}
}
