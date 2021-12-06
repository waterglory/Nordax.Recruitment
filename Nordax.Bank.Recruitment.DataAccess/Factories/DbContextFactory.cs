namespace Nordax.Bank.Recruitment.DataAccess.Factories
{
    public interface IDbContextFactory
    {
        ApplicationDbContext Create();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly ApplicationDbContext _dbContext;

        public DbContextFactory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ApplicationDbContext Create()
        {
            return _dbContext;
        }
    }
}