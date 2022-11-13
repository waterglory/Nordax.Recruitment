using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;

namespace Nordax.Bank.Recruitment.DataAccess.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddFactories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        }

        private static void AddFactories(this IServiceCollection services)
        {
            services.AddScoped<IDbContextFactory, DbContextFactory>();
        }
    }
}