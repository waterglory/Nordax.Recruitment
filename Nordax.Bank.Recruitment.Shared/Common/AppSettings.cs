using Microsoft.Extensions.Configuration;

namespace Nordax.Bank.Recruitment.Shared.Common
{
    public interface IAppSettings
    {
        public string Environment { get; }
        public string ApiUrlBase { get; }
    }

    public class AppSettings : IAppSettings
    {
        public AppSettings(IConfiguration configuration)
        {
            Environment = configuration["Environment"];
            ApiUrlBase = configuration["ApiUrlBase"];
        }

        public string Environment { get; }
        public string ApiUrlBase { get; }
    }
}