using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication;

namespace Nordax.Bank.Recruitment.DataAccess.DbContexts
{
	public class LoanApplicationDbContext : DbContext
	{
		public LoanApplicationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<LoanApplication> LoanApplications { get; set; }
		public DbSet<Applicant> Applicants { get; set; }
		public DbSet<Loan> Loans { get; set; }
		public DbSet<Document> Documents { get; set; }
	}
}
