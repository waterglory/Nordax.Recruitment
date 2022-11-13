using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities.Option;

namespace Nordax.Bank.Recruitment.DataAccess.DbContexts
{
	public class OptionDbContext : DbContext
	{
		public OptionDbContext(DbContextOptions<OptionDbContext> options) : base(options) { }

		public DbSet<BindingPeriod> BindingPeriods { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<BindingPeriod>().HasData(
				new BindingPeriod { Length = 1, InterestRate = 2M },
				new BindingPeriod { Length = 3, InterestRate = 2.3M },
				new BindingPeriod { Length = 6, InterestRate = 2.8M },
				new BindingPeriod { Length = 12, InterestRate = 3.3M },
				new BindingPeriod { Length = 24, InterestRate = 4M }
			);
		}
	}
}
