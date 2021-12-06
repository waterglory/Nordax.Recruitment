using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nordax.Bank.Recruitment.DataAccess.Configuration;
using Nordax.Bank.Recruitment.Domain.Configuration;
using Nordax.Bank.Recruitment.Shared.Common;

namespace Nordax.Bank.Recruitment.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings(configuration);
            services.AddSingleton<IAppSettings>(appSettings);
            services.AddEntityFramework(configuration);

            services.AddDataAccessServices();
            services.AddDomainServices();
        }
    }
}