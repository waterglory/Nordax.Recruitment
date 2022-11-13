using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			RunEfMigrationOnDebug(host);
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
				.UseDefaultServiceProvider((context, options) =>
				{
					options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
					options.ValidateOnBuild = true;
				});
		}

		public static IHost RunEfMigrationOnDebug(IHost host)
		{
#if DEBUG
			using (var scope = host.Services.CreateScope())
			{
				var dbContexts = new List<DbContext>
				{
					scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>(),
					scope.ServiceProvider.GetRequiredService<LoanApplicationDbContext>(),
					scope.ServiceProvider.GetRequiredService<CustomerDbContext>(),
					scope.ServiceProvider.GetRequiredService<FileDbContext>()
				};
				dbContexts.ForEach(ctx => ctx.Database.Migrate());
			}
#endif
			return host;
		}
	}
}