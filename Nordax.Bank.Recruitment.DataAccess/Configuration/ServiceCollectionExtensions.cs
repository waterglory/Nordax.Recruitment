using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;

namespace Nordax.Bank.Recruitment.DataAccess.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SubscriptionDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SubscriptionConnection")));
			services.AddDbContext<LoanApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LoanApplicationConnection")));
			services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CustomerConnection")));
			services.AddDbContext<FileDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("FileConnection")));
			services.AddDbContext<OptionDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("OptionConnection")));
		}

        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddFactories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
			services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IFileRepository, FileRepository>();
			services.AddScoped<IOptionRepository, OptionRepository>();
		}

        private static void AddFactories(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionDbContextFactory, SubscriptionDbContextFactory>();
			services.AddScoped<ILoanApplicationDbContextFactory, LoanApplicationDbContextFactory>();
		}
    }
}