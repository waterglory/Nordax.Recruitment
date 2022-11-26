using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Domain.Providers;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Domain.Validators;
using Nordax.Bank.Recruitment.Shared.Common.Constants;
using Nordax.Bank.Recruitment.Shared.Exceptions;
using Nordax.Bank.Recruitment.Shared.Models;
using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Tests.ServiceTests
{
	[TestClass]
	public class LoanApplicationServiceTests
	{
		private readonly Mock<ILoanApplicationRepository> _loanApplicationRepositoryMock;
		private readonly Mock<ILoanApplicationValidator> _loanApplicationValidatorMock;
		private readonly Mock<ICustomerProvider> _customerProviderMock;
		private readonly Mock<IFileStoreProvider> _fileStoreProviderMock;
		private readonly ILoanApplicationService _loanApplicatonService;

		private readonly Random _random = new Random((int)(DateTimeOffset.Now.ToUnixTimeSeconds() % int.MaxValue));

		public LoanApplicationServiceTests()
		{
			_loanApplicationRepositoryMock = new Mock<ILoanApplicationRepository>();
			_loanApplicationValidatorMock = new Mock<ILoanApplicationValidator>();
			_customerProviderMock = new Mock<ICustomerProvider>();
			_fileStoreProviderMock = new Mock<IFileStoreProvider>();
			_loanApplicatonService = new LoanApplicationService(
				_loanApplicationRepositoryMock.Object,
				new List<ILoanApplicationValidator> { _loanApplicationValidatorMock.Object },
				_customerProviderMock.Object,
				_fileStoreProviderMock.Object);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_loanApplicationRepositoryMock.Reset();
			_loanApplicationValidatorMock.Reset();
			_customerProviderMock.Reset();
			_fileStoreProviderMock.Reset();
		}

		private string GenerateGuidOfLength(int length = 32) =>
			Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, length);

		private LoanApplicationModel NewLoanApplication() =>
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

		[TestMethod]
		public async Task CheckOngoingApplication_NoData_ShouldNotThrowException()
		{
			await _loanApplicatonService.CheckOngoingApplication("1234567890");
		}

		[TestMethod]
		public async Task CheckOngoingApplication_ApplicantHasOngoingApplication_ShouldThrowException()
		{
			var organizationNo = "1234567890";
			var caseNo = "51244097567179";
			_loanApplicationRepositoryMock.Setup(e => e.GetOngoingLoanApplication(organizationNo, It.IsAny<string>()))
				.ReturnsAsync(new LoanApplicationModel { CaseNo = caseNo });
			var exception = await Assert.ThrowsExceptionAsync<CustomerOngoingLoanApplicationException>(() => _loanApplicatonService.CheckOngoingApplication(organizationNo));
			Assert.AreEqual(caseNo, exception.Message);
		}

		[TestMethod]
		public async Task SubmitLoanApplication_ValidationHits_ShouldThrowException()
		{
			string reason = "Some values are invalid.";
			_loanApplicationValidatorMock.Setup(e => e.Validate(It.IsAny<LoanApplicationModel>(), out reason))
				.Returns(false);
			var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => _loanApplicatonService.SubmitLoanApplication(NewLoanApplication()));
			Assert.AreEqual(reason, exception.Message);
		}

		[TestMethod]
		public async Task SubmitLoanApplication_ApplicantHasOngoingApplication_ShouldThrowException()
		{
			var loanApplication = NewLoanApplication();
			var caseNo = "51244097567179";
			_loanApplicationRepositoryMock.Setup(e => e.GetOngoingLoanApplication(loanApplication.Applicant.OrganizationNo, It.IsAny<string>()))
				.ReturnsAsync(new LoanApplicationModel { CaseNo = caseNo });
			string reason = null;
			_loanApplicationValidatorMock.Setup(e => e.Validate(It.IsAny<LoanApplicationModel>(), out reason))
				.Returns(true);
			var exception = await Assert.ThrowsExceptionAsync<CustomerOngoingLoanApplicationException>(() => _loanApplicatonService.SubmitLoanApplication(loanApplication));
			Assert.AreEqual(caseNo, exception.Message);
		}

		[TestMethod]
		public async Task SubmitLoanApplication_AllChecksPassed_ShouldReturnCaseNo()
		{
			string reason = null;
			_loanApplicationValidatorMock.Setup(e => e.Validate(It.IsAny<LoanApplicationModel>(), out reason))
				.Returns(true);
			var caseNo = await _loanApplicatonService.SubmitLoanApplication(NewLoanApplication());
			Assert.IsFalse(string.IsNullOrWhiteSpace(caseNo));
		}

		[TestMethod]
		public async Task SubmitLoanApplication_UpdateCustomerProviderDataFailed_ShouldReturnCaseNo()
		{
			_customerProviderMock.Setup(e => e.MergeCustomer(It.IsAny<CustomerModel>())).Throws(new Exception("Merge customer failed."));
			string reason = null;
			_loanApplicationValidatorMock.Setup(e => e.Validate(It.IsAny<LoanApplicationModel>(), out reason))
				.Returns(true);
			var caseNo = await _loanApplicatonService.SubmitLoanApplication(NewLoanApplication());
			Assert.IsFalse(string.IsNullOrWhiteSpace(caseNo));
		}

		[TestMethod]
		public async Task GetAllLoanApplications_NoData_ShouldBeEmpty()
		{
			_loanApplicationRepositoryMock.Setup(e => e.GetAllLoanApplications()).ReturnsAsync(new List<LoanApplicationModel>());
			var loanApplications = await _loanApplicatonService.GetAllLoanApplications();
			Assert.IsNotNull(loanApplications);
			Assert.AreEqual(0, loanApplications.Count);
		}

		[TestMethod]
		public async Task GetAllLoanApplications_SomeData_ShouldReturnItems()
		{
			var testLoanApplications = new List<LoanApplicationModel>
			{
				NewLoanApplication(),
				NewLoanApplication(),
				NewLoanApplication(),
			};
			_loanApplicationRepositoryMock.Setup(e => e.GetAllLoanApplications()).ReturnsAsync(testLoanApplications);
			var loanApplications = await _loanApplicatonService.GetAllLoanApplications();
			Assert.IsNotNull(loanApplications);
			Assert.AreEqual(testLoanApplications.Count, loanApplications.Count);
			testLoanApplications.ForEach(testLoanApplication =>
			{
				var loanApplication = loanApplications.SingleOrDefault(la => la.CaseNo == testLoanApplication.CaseNo);
				if (loanApplication == null)
					Assert.Fail("Loan application with the same case number is not found.");
				Assert.AreEqual(testLoanApplication.CaseNo, loanApplication.CaseNo);
				Assert.AreEqual(testLoanApplication.CurrentStep, loanApplication.CurrentStep);
				Assert.AreEqual(testLoanApplication.Applicant.OrganizationNo, loanApplication.Applicant.OrganizationNo);
				Assert.AreEqual(testLoanApplication.Applicant.FirstName, loanApplication.Applicant.FirstName);
				Assert.AreEqual(testLoanApplication.Applicant.Surname, loanApplication.Applicant.Surname);
				Assert.AreEqual(testLoanApplication.Applicant.PhoneNo, loanApplication.Applicant.PhoneNo);
				Assert.AreEqual(testLoanApplication.Applicant.Email, loanApplication.Applicant.Email);
				Assert.AreEqual(testLoanApplication.Applicant.Address, loanApplication.Applicant.Address);
				Assert.AreEqual(testLoanApplication.Applicant.IncomeLevel, loanApplication.Applicant.IncomeLevel);
				Assert.AreEqual(testLoanApplication.Applicant.IsPoliticallyExposed, loanApplication.Applicant.IsPoliticallyExposed);
				Assert.AreEqual(testLoanApplication.Loan.Amount, testLoanApplication.Loan.Amount);
				Assert.AreEqual(testLoanApplication.Loan.BindingPeriod, testLoanApplication.Loan.BindingPeriod);
				Assert.AreEqual(testLoanApplication.Loan.InterestRate, testLoanApplication.Loan.InterestRate);
				Assert.AreEqual(testLoanApplication.Loan.PaymentPeriod, testLoanApplication.Loan.PaymentPeriod);
				foreach (var talDoc in testLoanApplication.Documents)
				{
					var loanApplicationDoc = loanApplication.Documents.FirstOrDefault(ad => ad.DocumentType == talDoc.DocumentType);
					if (loanApplicationDoc == null)
						Assert.Fail("Document is missing.");
					Assert.AreEqual(talDoc.FileRef, loanApplicationDoc.FileRef);
				}
			});
		}

		[TestMethod]
		public async Task GetDocumentContent_DocumentExists_ShouldReturnDocument()
		{
			var documentId = Guid.NewGuid();
			var fileRef = $"document/{GenerateGuidOfLength()}";
			var testFile = new FileModel
			{
				FileRef = fileRef,
				FileName = "TestFileName",
				FileType = "tst",
				Content = new byte[5],
				ContentType = "test/document"
			};
			_loanApplicationRepositoryMock.Setup(e => e.GetDocument(documentId)).ReturnsAsync(new DocumentModel { FileRef = fileRef });
			_fileStoreProviderMock.Setup(e => e.GetFile(fileRef)).ReturnsAsync(testFile);
			var file = await _loanApplicatonService.GetDocumentContent(documentId);
			Assert.IsNotNull(file);
			Assert.AreEqual(testFile.FileRef, file.FileRef);
			Assert.AreEqual(testFile.FileName, file.FileName);
			Assert.AreEqual(testFile.FileType, file.FileType);
			Assert.AreEqual(testFile.Content.Length, file.Content.Length);
			for (int i = 0; i < testFile.Content.Length; i++)
				Assert.AreEqual(testFile.Content[i], file.Content[i]);
			Assert.AreEqual(testFile.ContentType, file.ContentType);
		}
	}
}
