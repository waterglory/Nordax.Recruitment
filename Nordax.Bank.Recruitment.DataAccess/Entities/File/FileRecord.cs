using Nordax.Bank.Recruitment.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.File
{
	public class FileRecord
	{
		public FileRecord() { }

		public FileRecord(FileModel model)
		{
			FileRef = model.FileRef;
			FromDomainModel(model);
		}

		[Key][MaxLength(100)] public string FileRef { get; set; }

		[Required][MaxLength(100)] public string FileName { get; set; }

		[Required] public byte[] Content { get; set; }

		[Required][MaxLength(20)] public string FileType { get; set; }

		public FileModel ToDomainModel() =>
			new()
			{
				FileRef = FileRef,
				FileName = FileName,
				Content = Content,
				FileType = FileType
			};

		public void FromDomainModel(FileModel model)
		{
			FileName = model.FileName;
			Content = model.Content;
			FileType = model.FileType;
		}
	}
}
