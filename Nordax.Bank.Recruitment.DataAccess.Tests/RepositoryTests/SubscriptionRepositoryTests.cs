using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.Entities;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.DataAccess.Tests.Configuration;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.RepositoryTests
{
    [TestClass]
    public class SubscriptionRepositoryTests
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ApplicationDbContext _testDbContext;

        public SubscriptionRepositoryTests()
        {
            var dbContextFactoryMock = new Mock<IDbContextFactory>();

            _testDbContext = EfConfig.CreateInMemoryTestDbContext();
            dbContextFactoryMock.Setup(d => d.Create()).Returns(EfConfig.CreateInMemoryApplicationDbContext());

            _subscriptionRepository = new SubscriptionRepository(dbContextFactoryMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _testDbContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task RegisterSubscriptionAsync_EmailExists_SHouldThrowException()
        {
            await _testDbContext.Subscriptions.AddAsync(new Subscription("firstNAme", "email@email.email"));
            await _testDbContext.SaveChangesAsync();

            await Assert.ThrowsExceptionAsync<EmailAlreadyRegisteredException>(() => _subscriptionRepository.RegisterSubscriptionAsync("first", "email@email.email"));
        }

        [TestMethod]
        public async Task RegisterSubscriptionAsync_NewEmail_ShouldAddSubscription()
        {
            await _subscriptionRepository.RegisterSubscriptionAsync("first", "email@email.email");

            var subscription = await _testDbContext.Subscriptions.SingleAsync();

            Assert.AreEqual("first", subscription.Name);
            Assert.AreEqual("email@email.email", subscription.Email);
        }
    }
}