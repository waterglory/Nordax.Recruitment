using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.Option;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.DataAccess.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.RepositoryTests
{
	[TestClass]
	public class OptionRepositoryTests
	{
		private readonly IOptionRepository _optionRepository;
		private readonly OptionDbContext _testDbContext;

		public OptionRepositoryTests()
		{
			var dbContextFactoryMock = new Mock<IOptionDbContextFactory>();

			_testDbContext = EfConfig.Option.CreateInMemoryTestDbContext();
			dbContextFactoryMock.Setup(d => d.Create()).Returns(EfConfig.Option.CreateInMemoryDbContext());

			_optionRepository = new OptionRepository(dbContextFactoryMock.Object);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_testDbContext.Database.EnsureDeleted();
		}

		[TestMethod]
		public async Task GetBindingPeriods_NoData_ShouldBeEmpty()
		{
			var bindingPeriods = await _optionRepository.GetBindingPeriods();

			Assert.IsNotNull(bindingPeriods);
			Assert.AreEqual(0, bindingPeriods.Count);
		}

		[TestMethod]
		public async Task GetBindingPeriods_SomeItems_ShouldReturnItems()
		{
			var testPeriods = new List<BindingPeriod>
			{
				new BindingPeriod { Length = 1, InterestRate = 1m },
				new BindingPeriod { Length = 3, InterestRate = 2m },
				new BindingPeriod { Length = 6, InterestRate = 3m }
			};
			testPeriods.ForEach(bp => _testDbContext.BindingPeriods.Add(bp));
			await _testDbContext.SaveChangesAsync();

			var bindingPeriods = await _optionRepository.GetBindingPeriods();

			Assert.IsNotNull(bindingPeriods);
			Assert.AreEqual(bindingPeriods.Count, bindingPeriods.Count);
			testPeriods.ForEach(tp =>
			{
				Assert.IsTrue(bindingPeriods.Any(bp => bp.Length == tp.Length && bp.InterestRate == tp.InterestRate));
			});
		}
	}
}
