using Microsoft.Extensions.DependencyInjection;
using Nordax.Bank.Recruitment.Domain.Services;

namespace Nordax.Bank.Recruitment.Domain.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionService, SubscriptionService>();
        }
    }
}