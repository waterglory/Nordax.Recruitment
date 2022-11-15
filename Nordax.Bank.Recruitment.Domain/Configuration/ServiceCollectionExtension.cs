using Microsoft.Extensions.DependencyInjection;
using Nordax.Bank.Recruitment.Domain.Providers;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Domain.Validators;

namespace Nordax.Bank.Recruitment.Domain.Configuration
{
	public static class ServiceCollectionExtension
	{
		public static void AddDomainServices(this IServiceCollection services)
		{
			services.AddServices();
			services.AddProviders();
			services.AddValidators();
		}

		private static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<ISubscriptionService, SubscriptionService>()
				.AddScoped<IOptionService, OptionService>()
				.AddScoped<ILoanApplicationService, LoanApplicationService>();
		}

		private static void AddProviders(this IServiceCollection services)
		{
			services.AddScoped<ICustomerProvider, CustomerDbProvider>()
				.AddScoped<IFileStoreProvider, FileDbStoreProvider>();
		}

		private static void AddValidators(this IServiceCollection services)
		{
			services.AddScoped<ILoanApplicationValidator, OrganizationNoValidator>()
				.AddScoped<ILoanApplicationValidator, NameValidator>()
				.AddScoped<ILoanApplicationValidator, EmailValidator>()
				.AddScoped<ILoanApplicationValidator, PhoneValidator>()
				.AddScoped<ILoanApplicationValidator, LoanAmountValidator>()
				.AddScoped<ILoanApplicationValidator, BindingPeriodValidator>()
				.AddScoped<ILoanApplicationValidator, DocumentsValidator>();
		}
	}
}