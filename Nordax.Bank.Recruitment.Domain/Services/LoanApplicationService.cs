using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Domain.Providers;
using Nordax.Bank.Recruitment.Domain.Validators;
using Nordax.Bank.Recruitment.Shared.Exceptions;
using Nordax.Bank.Recruitment.Shared.Models;
using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Services
{
	public interface ILoanApplicationService
	{
		Task<string> SubmitLoanApplication(LoanApplicationModel model);
		Task<List<LoanApplicationModel>> GetAllLoanApplications();
		Task<FileModel> GetDocumentContent(Guid documentId);
	}

	public class LoanApplicationService : ILoanApplicationService
	{
		private ILoanApplicationRepository _loanApplicationRepository;
		private IEnumerable<ILoanApplicationValidator> _loanApplicationValidators;

		private ICustomerProvider _customerProvider;
		private IFileStoreProvider _fileStoreProvider;

		public LoanApplicationService(
			ILoanApplicationRepository loanApplicationRepository,
			IEnumerable<ILoanApplicationValidator> loanApplicationValidators,
			ICustomerProvider customerProvider,
			IFileStoreProvider fileStoreProvider)
		{
			_loanApplicationRepository = loanApplicationRepository;
			_loanApplicationValidators = loanApplicationValidators;

			_customerProvider = customerProvider;
			_fileStoreProvider = fileStoreProvider;
		}

		private string GenerateCaseNo() =>
			$"{DateTime.UtcNow.ToString("MMhhyymmddss000fff")}";

		private void Validate(LoanApplicationModel model)
		{
			if (model == null) throw new ArgumentNullException("LoanApplication");
			if (model.Applicant == null) throw new ArgumentNullException(nameof(model.Applicant));
			if (model.Loan == null) throw new ArgumentNullException(nameof(model.Loan));
			if (model.Documents == null) throw new ArgumentNullException(nameof(model.Documents));

			string reason = null;
			if (_loanApplicationValidators.Any(v => !v.Validate(model, out reason)))
				throw new ValidationException(reason);
		}

		private Task UpdateCustomer(ApplicantModel applicant) =>
			_customerProvider.MergeCustomer(new CustomerModel
			{
				OrganizationNo = applicant.OrganizationNo,
				FirstName = applicant.FirstName,
				Surname = applicant.Surname,
				Email = applicant.Email,
				PhoneNo = applicant.PhoneNo,
				Address = applicant.Address,
				IncomeLevel = applicant.IncomeLevel,
				IsPoliticallyExposed = applicant.IsPoliticallyExposed,
			});

		public async Task<string> SubmitLoanApplication(LoanApplicationModel model)
		{
			Validate(model);

			model.Id = Guid.Empty;
			model.CaseNo = GenerateCaseNo();
			await _loanApplicationRepository.SaveLoanApplication(model);

			await UpdateCustomer(model.Applicant);

			return model.CaseNo;
		}

		public Task<List<LoanApplicationModel>> GetAllLoanApplications() =>
			_loanApplicationRepository.GetAllLoanApplications();

		public async Task<FileModel> GetDocumentContent(Guid documentId)
		{
			var document = await _loanApplicationRepository.GetDocument(documentId);
			return await _fileStoreProvider.GetFile(document.FileRef);
		}
	}
}
