using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.DataAccess.Entities.File
{
	public class FileRecord
	{
		[Key][MaxLength(100)] public string FileRef { get; set; }

		[Required] public byte[] Content { get; set; }

		[Required][MaxLength(20)] public string FileType { get; set; }
	}
}
