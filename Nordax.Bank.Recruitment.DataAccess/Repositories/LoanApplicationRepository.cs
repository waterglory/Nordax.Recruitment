using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.Shared.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
	public interface ILoanApplicationRepository
	{
		public Task<Guid> SubmitLoanApplication(LoanApplicationModel model);
		public Task<List<LoanApplicationModel>> GetAllLoanApplications();
		public Task<DocumentModel> GetDocument(Guid documentId);
	}

	public class LoanApplicationRepository : ILoanApplicationRepository
	{
		private readonly LoanApplicationDbContext _dbContext;

		public LoanApplicationRepository(ILoanApplicationDbContextFactory dbContextFactory)
		{
			_dbContext = dbContextFactory.Create();
		}

		public async Task<Guid> SubmitLoanApplication(LoanApplicationModel model)
		{
			var newLoanApplication = _dbContext.Add(new LoanApplication(model));
			await _dbContext.SaveChangesAsync();
			return newLoanApplication.Entity.Id;
		}

		public async Task<List<LoanApplicationModel>> GetAllLoanApplications() =>
			await _dbContext.LoanApplications
				.Include(la => la.Applicant)
				.Include(la => la.Loan)
				.Include(la => la.Documents)
				.Select(la => la.ToDomainModel())
				.ToListAsync();

		public async Task<DocumentModel> GetDocument(Guid documentId)
		{
			var document = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == documentId);
			if (document == null) throw new LoanDocumentNotFoundException();
			return document.ToDomainModel();
		}
	}
}
