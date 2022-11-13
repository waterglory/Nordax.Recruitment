using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities.Customer;

namespace Nordax.Bank.Recruitment.DataAccess.DbContexts
{
	public class CustomerDbContext : DbContext
	{
		public CustomerDbContext(DbContextOptions options) : base(options) { }

		public DbSet<CustomerInfo> CustomerInfos { get; set; }
	}
}
