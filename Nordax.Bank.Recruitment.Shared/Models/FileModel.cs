namespace Nordax.Bank.Recruitment.Shared.Models
{
	public class FileModel
	{
		public string FileRef { get; set; }

		public string FileName { get; set; }

		public string ContentType { get; set; }

		public byte[] Content { get; set; }

		public string FileType { get; set; }
	}
}
