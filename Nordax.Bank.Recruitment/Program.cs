using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using System.Runtime.CompilerServices;

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
				var db = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();
				db.Database.Migrate();
			}
#endif
			return host;
		}
	}
}