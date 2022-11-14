using Nordax.Bank.Recruitment.DataAccess.DbContexts;

namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
	public interface IFileDbContextFactory
	{
		FileDbContext Create();
	}

	public class FileDbContextFactory : DbContextFactory<FileDbContext>, IFileDbContextFactory
	{
		public FileDbContextFactory(FileDbContext dbContext) : base(dbContext) { }
	}
}
