using Microsoft.Extensions.Configuration;
using Nordax.Bank.Recruitment.Shared.Exceptions;
using System.Linq;

namespace Nordax.Bank.Recruitment.Shared.Common
{
	public class FileSettings
	{
		public int MaxFileSize { get; internal set; }
		public string[] SupportedExtensions { get; internal set; }
	}

	public interface IAppSettings
	{
		public string Environment { get; }
		public string ApiUrlBase { get; }
		public FileSettings FileSettings { get; }
	}

	public class AppSettings : IAppSettings
	{
		public AppSettings(IConfiguration configuration)
		{
			Environment = configuration["Environment"];
			ApiUrlBase = configuration["ApiUrlBase"];

			FileSettings = new FileSettings();
			LoadFileSettings(configuration, FileSettings);
		}

		public string Environment { get; }
		public string ApiUrlBase { get; }

		public FileSettings FileSettings { get; }

		private void LoadFileSettings(IConfiguration configuration, FileSettings fileSettings)
		{
			const string maxFileSizeKey = "FileSettings:MaxFileSize";
			int maxFileSize;
			if (!int.TryParse(configuration["FileSettings:MaxFileSize"], out maxFileSize))
				throw new InvalidConfigurationException($"{maxFileSizeKey} must be an integer.");

			fileSettings.MaxFileSize = maxFileSize;
			fileSettings.SupportedExtensions = configuration.GetSection("FileSettings:SupportedExtensions")
				.GetChildren()
				.Select(c => c.Value)
				.ToArray();
		}
	}
}