using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities.File;

namespace Nordax.Bank.Recruitment.DataAccess.DbContexts
{
	public class FileDbContext : DbContext
	{
		public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }

		public DbSet<FileRecord> FileRecords { get; set; }
	}
}
