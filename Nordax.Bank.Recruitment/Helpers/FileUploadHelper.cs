using Nordax.Bank.Recruitment.Shared.Common;
using MimeDetective;
using System.IO;
using System.Linq;
using MimeDetective.Storage;
using System.Collections.Immutable;
using System;
using Microsoft.AspNetCore.Http;

namespace Nordax.Bank.Recruitment.Helpers
{
	public interface IFileUploadHelper
	{
		bool ValidateRequirement(IFormFile formFile, out string reason);
		bool VerifySignature(string fileExtension, byte[] content);
	}

	public class FileUploadHelper : IFileUploadHelper
	{
		private FileSettings _fileSettings;
		private ContentInspector _inspector;
		private string _flattenedSupportedExtensions;

		public FileUploadHelper(IAppSettings appSettings)
		{
			_fileSettings = appSettings.FileSettings;
			_inspector = BuildInspector(appSettings.FileSettings.SupportedExtensions);
			_flattenedSupportedExtensions = string.Join(',', appSettings.FileSettings.SupportedExtensions);
		}

		private ContentInspector BuildInspector(string[] supportedExtensions)
		{
			var allDefinitions = MimeDetective.Definitions.Default.All();

			var extensions = supportedExtensions.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);

			var scopedDefinitions = allDefinitions
				.ScopeExtensions(extensions)
				.TrimMeta()
				.TrimDescription()
				.TrimMimeType()
				.ToImmutableArray();

			return new ContentInspectorBuilder()
			{
				Definitions = scopedDefinitions
			}.Build();
		}

		public bool ValidateRequirement(IFormFile formFile, out string reason)
		{
			if (formFile.Length > _fileSettings.MaxFileSize)
			{
				reason = $"File size exceeding {_fileSettings.MaxFileSize}.";
				return false;
			}

			var fileExtension = Path.GetExtension(formFile.FileName).TrimStart('.');
			var supportedExts = _fileSettings.SupportedExtensions;
			if (supportedExts.All(e => !e.Equals(fileExtension, System.StringComparison.InvariantCultureIgnoreCase)))
			{
				reason = $"Unsupported file type {fileExtension}, please provide a file within the following types: {_flattenedSupportedExtensions}.";
				return false;
			}

			reason = null;
			return true;
		}

		public bool VerifySignature(string fileExtension, byte[] content) =>
			_inspector.Inspect(content)
				.ByFileExtension()
				.Any(e => e.Extension == fileExtension);
	}
}
