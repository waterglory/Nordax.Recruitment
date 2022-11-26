using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication;
using Nordax.Bank.Recruitment.DataAccess.Entities.Option;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.DataAccess.Tests.Configuration;
using Nordax.Bank.Recruitment.Shared.Common.Constants;
using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.RepositoryTests
{
	[TestClass]
	public class LoanApplicationRepositoryTests
	{
		private readonly ILoanApplicationRepository _loanApplicationRepository;
		private readonly LoanApplicationDbContext _testDbContext;
		private readonly Random _random = new Random((int)(DateTimeOffset.Now.ToUnixTimeSeconds() % int.MaxValue));

		public LoanApplicationRepositoryTests()
		{
			var dbContextFactoryMock = new Mock<ILoanApplicationDbContextFactory>();

			_testDbContext = EfConfig.LoanApplication.CreateInMemoryTestDbContext();
			dbContextFactoryMock.Setup(d => d.Create()).Returns(EfConfig.LoanApplication.CreateInMemoryDbContext());

			_loanApplicationRepository = new LoanApplicationRepository(dbContextFactoryMock.Object);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_testDbContext.Database.EnsureDeleted();
		}

		private string GenerateGuidOfLength(int length = 32) =>
			Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, length);

		private LoanApplicationModel NewLoanApplicationModel() =>
			new LoanApplicationModel()
			{
				CaseNo = GenerateGuidOfLength(20),
				CurrentStep = LoanApplicationStep.Verification,
				Applicant = new ApplicantModel
				{
					OrganizationNo = GenerateGuidOfLength(12),
					FirstName = GenerateGuidOfLength(),
					Surname = GenerateGuidOfLength(),
					PhoneNo = GenerateGuidOfLength(20),
					Email = GenerateGuidOfLength(),
					Address = GenerateGuidOfLength(),
					IncomeLevel = GenerateGuidOfLength(20),
					IsPoliticallyExposed = _random.Next() % 2 == 0 ? true : false
				},
				Loan = new LoanModel
				{
					Amount = _random.Next(100000, 3000000),
					BindingPeriod = _random.Next(1, 24),
					InterestRate = Convert.ToDecimal(_random.NextDouble() * 8),
					PaymentPeriod = _random.Next(6, 60),
				},
				Documents = new List<DocumentModel>
				{
					new DocumentModel
					{
						DocumentType = GenerateGuidOfLength(20),
						FileRef = GenerateGuidOfLength()
					}
				}
			};

		private LoanApplication NewLoanApplicationEntity() =>
			new LoanApplication()
			{
				CaseNo = GenerateGuidOfLength(20),
				CurrentStep = LoanApplicationStep.Verification,
				Applicant = new Applicant
				{
					OrganizationNo = GenerateGuidOfLength(12),
					FirstName = GenerateGuidOfLength(),
					Surname = GenerateGuidOfLength(),
					PhoneNo = GenerateGuidOfLength(20),
					Email = GenerateGuidOfLength(),
					Address = GenerateGuidOfLength(),
					IncomeLevel = GenerateGuidOfLength(20),
					IsPoliticallyExposed = _random.Next() % 2 == 0 ? true : false
				},
				Loan = new Loan
				{
					Amount = _random.Next(100000, 3000000),
					BindingPeriod = _random.Next(1, 24),
					InterestRate = Convert.ToDecimal(_random.NextDouble() * 8),
					PaymentPeriod = _random.Next(6, 60),
				},
				Documents = new List<Document>
				{
					new Document
					{
						DocumentType = GenerateGuidOfLength(20),
						FileRef = GenerateGuidOfLength()
					}
				}
			};

		private void CompareModelToEntity(LoanApplicationModel expected, LoanApplication actual)
		{
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.CaseNo, actual.CaseNo);
			Assert.AreEqual(expected.CurrentStep, actual.CurrentStep);
			Assert.AreEqual(expected.Applicant.OrganizationNo, actual.Applicant.OrganizationNo);
			Assert.AreEqual(expected.Applicant.FirstName, actual.Applicant.FirstName);
			Assert.AreEqual(expected.Applicant.Surname, actual.Applicant.Surname);
			Assert.AreEqual(expected.Applicant.PhoneNo, actual.Applicant.PhoneNo);
			Assert.AreEqual(expected.Applicant.Email, actual.Applicant.Email);
			Assert.AreEqual(expected.Applicant.Address, actual.Applicant.Address);
			Assert.AreEqual(expected.Applicant.IncomeLevel, actual.Applicant.IncomeLevel);
			Assert.AreEqual(expected.Applicant.IsPoliticallyExposed, actual.Applicant.IsPoliticallyExposed);
			Assert.AreEqual(expected.Loan.Amount, expected.Loan.Amount);
			Assert.AreEqual(expected.Loan.BindingPeriod, expected.Loan.BindingPeriod);
			Assert.AreEqual(expected.Loan.InterestRate, expected.Loan.InterestRate);
			Assert.AreEqual(expected.Loan.PaymentPeriod, expected.Loan.PaymentPeriod);
			foreach (var expectedDoc in expected.Documents)
			{
				var actualDoc = actual.Documents.FirstOrDefault(ad => ad.DocumentType == expectedDoc.DocumentType);
				if (actualDoc == null)
					Assert.Fail("Document is missing.");
				Assert.AreEqual(expectedDoc.FileRef, actualDoc.FileRef);
			}
		}

		private void CompareEntityToModel(LoanApplication expected, LoanApplicationModel actual)
		{
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.CaseNo, actual.CaseNo);
			Assert.AreEqual(expected.CurrentStep, actual.CurrentStep);
			Assert.AreEqual(expected.Applicant.OrganizationNo, actual.Applicant.OrganizationNo);
			Assert.AreEqual(expected.Applicant.FirstName, actual.Applicant.FirstName);
			Assert.AreEqual(expected.Applicant.Surname, actual.Applicant.Surname);
			Assert.AreEqual(expected.Applicant.PhoneNo, actual.Applicant.PhoneNo);
			Assert.AreEqual(expected.Applicant.Email, actual.Applicant.Email);
			Assert.AreEqual(expected.Applicant.Address, actual.Applicant.Address);
			Assert.AreEqual(expected.Applicant.IncomeLevel, actual.Applicant.IncomeLevel);
			Assert.AreEqual(expected.Applicant.IsPoliticallyExposed, actual.Applicant.IsPoliticallyExposed);
			Assert.AreEqual(expected.Loan.Amount, expected.Loan.Amount);
			Assert.AreEqual(expected.Loan.BindingPeriod, expected.Loan.BindingPeriod);
			Assert.AreEqual(expected.Loan.InterestRate, expected.Loan.InterestRate);
			Assert.AreEqual(expected.Loan.PaymentPeriod, expected.Loan.PaymentPeriod);
			foreach (var expectedDoc in expected.Documents)
			{
				var actualDoc = actual.Documents.FirstOrDefault(ad => ad.DocumentType == expectedDoc.DocumentType);
				if (actualDoc == null)
					Assert.Fail("Document is missing.");
				Assert.AreEqual(expectedDoc.FileRef, actualDoc.FileRef);
			}
		}

		private IQueryable<LoanApplication> IncludeAllLoanApplicationData() =>
			_testDbContext.LoanApplications
				.Include(la => la.Applicant)
				.Include(la => la.Loan)
				.Include(la => la.Documents);

		[TestMethod]
		public async Task SaveLoanApplication_NullArgument_ShouldThrowException()
		{
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _loanApplicationRepository.SaveLoanApplication(null));
			
		}

		[TestMethod]
		public async Task SaveLoanApplication_NewLoanApplication_ShouldAddLoanApplication()
		{
			var newLoanApplication = NewLoanApplicationModel();
			await _loanApplicationRepository.SaveLoanApplication(newLoanApplication);

			var savedLoanApplication = await IncludeAllLoanApplicationData().SingleOrDefaultAsync();

			CompareModelToEntity(newLoanApplication, savedLoanApplication);
		}

		/* How to test unique constraint? Or if entity setup is correct.
		 * [TestMethod]
		public async Task SaveLoanApplication_DuplicateCaseNo_ShouldThrowException()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			await _testDbContext.SaveChangesAsync();

			var loanApplication = NewLoanApplicationModel();
			loanApplication.CaseNo = testLoanApplication.CaseNo;

			var newLoanApplication = NewLoanApplicationModel();
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _loanApplicationRepository.SaveLoanApplication(loanApplication));
		}*/

		[TestMethod]
		public async Task GetAllLoanApplications_NoData_ShouldBeEmpty()
		{
			var loanApplications = await _loanApplicationRepository.GetAllLoanApplications();

			Assert.IsNotNull(loanApplications);
			Assert.AreEqual(0, loanApplications.Count);
		}

		[TestMethod]
		public async Task GetAllLoanApplications_SomeItems_ShouldReturnItems()
		{
			var testLoanApplications = new List<LoanApplication>
			{
				NewLoanApplicationEntity(),
				NewLoanApplicationEntity(),
				NewLoanApplicationEntity(),
			};
			testLoanApplications.ForEach(la => _testDbContext.LoanApplications.Add(la));
			await _testDbContext.SaveChangesAsync();

			var loanApplications = await _loanApplicationRepository.GetAllLoanApplications();

			Assert.IsNotNull(loanApplications);
			Assert.AreEqual(testLoanApplications.Count, loanApplications.Count);
			testLoanApplications.ForEach(tal =>
			{
				var loanApplication = loanApplications.SingleOrDefault(la => la.CaseNo == tal.CaseNo);
				if (loanApplication == null)
					Assert.Fail("Loan application with the same case number is not found.");
				CompareEntityToModel(tal, loanApplication);
			});
		}

		[TestMethod]
		public async Task GetDocument_DocumentNotExists_ShouldThrowException()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			await Assert.ThrowsExceptionAsync<LoanDocumentNotFoundException>(() => _loanApplicationRepository.GetDocument(Guid.NewGuid()));
		}

		[TestMethod]
		public async Task GetDocument_DocumentExists_ShouldReturnCorrectDocument()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();
			var testDocument = testLoanApplication.Documents.First();

			var document = await _loanApplicationRepository.GetDocument(testDocument.Id);

			Assert.IsNotNull(document);
			Assert.AreEqual(testDocument.Id, document.Id);
			Assert.AreEqual(testDocument.DocumentType, document.DocumentType);
			Assert.AreEqual(testDocument.FileRef, document.FileRef);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasLoanApplicationNoStepSpecified_ShouldBeNull()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(testLoanApplication.Applicant.OrganizationNo);

			Assert.IsNull(loanApplication);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasNoLoanApplicationNoStepSpecified_ShouldBeNull()
		{
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var applicantOrganizationNo = NewLoanApplicationModel().Applicant.OrganizationNo;

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(applicantOrganizationNo);

			Assert.IsNull(loanApplication);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasLoanApplicationUnknownStepSpecified_ShouldBeNull()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(testLoanApplication.Applicant.OrganizationNo, "Unknown");

			Assert.IsNull(loanApplication);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasNoLoanApplicationUnkownSpecified_ShouldBeNull()
		{
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var applicantOrganizationNo = NewLoanApplicationModel().Applicant.OrganizationNo;

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(applicantOrganizationNo, "Unknown");

			Assert.IsNull(loanApplication);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasNoLoanApplication_ShouldBeNull()
		{
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var applicantOrganizationNo = NewLoanApplicationModel().Applicant.OrganizationNo;

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(applicantOrganizationNo, LoanApplicationStep.Verification);

			Assert.IsNull(loanApplication);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasLoanApplication_ShouldReturnMatchingLoanApplication()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(testLoanApplication.Applicant.OrganizationNo, LoanApplicationStep.Verification);

			Assert.IsNotNull(loanApplication);
			Assert.AreEqual(testLoanApplication.CaseNo, loanApplication.CaseNo);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasNoLoanApplicationMultiStepsSpecified_ShouldBeNull()
		{
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var applicantOrganizationNo = NewLoanApplicationModel().Applicant.OrganizationNo;

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(applicantOrganizationNo,
				LoanApplicationStep.Verification,
				LoanApplicationStep.Approved,
				LoanApplicationStep.Rejected);

			Assert.IsNull(loanApplication);
		}

		[TestMethod]
		public async Task GetOngoingLoanApplication_ApplicantHasLoanApplicationMultiStepsSpecified_ShouldReturnMatchingLoanApplication()
		{
			var testLoanApplication = NewLoanApplicationEntity();
			_testDbContext.LoanApplications.Add(testLoanApplication);
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			_testDbContext.LoanApplications.Add(NewLoanApplicationEntity());
			await _testDbContext.SaveChangesAsync();

			var loanApplication = await _loanApplicationRepository.GetOngoingLoanApplication(testLoanApplication.Applicant.OrganizationNo, 
				LoanApplicationStep.Verification,
				LoanApplicationStep.Approved,
				LoanApplicationStep.Rejected);

			Assert.IsNotNull(loanApplication);
			Assert.AreEqual(testLoanApplication.CaseNo, loanApplication.CaseNo);
		}
	}
}
