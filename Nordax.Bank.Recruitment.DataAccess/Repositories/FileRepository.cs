using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.File;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
	public interface IFileRepository
	{
		Task<FileModel> GetFile(string fileRef);
		Task SaveFile(FileModel model, bool force = false);
	}

	public class FileRepository : IFileRepository
	{
		private readonly FileDbContext _dbContext;

		public FileRepository(IFileDbContextFactory dbContextFactory)
		{
			_dbContext = dbContextFactory.Create();
		}

		public async Task<FileModel> GetFile(string fileRef)
		{
			var file = await _dbContext.FileRecords.FirstOrDefaultAsync(f => f.FileRef == fileRef);
			if (file == null) throw new FileNotFoundException();
			return file.ToDomainModel();
		}

		public async Task SaveFile(FileModel model, bool force = false)
		{
			if (model == null) throw new ArgumentNullException();

			var existingFile = await _dbContext.FileRecords.FirstOrDefaultAsync(f => f.FileRef == model.FileRef);
			if (existingFile == null)
				_dbContext.Add(new FileRecord(model));
			else if (force)
				existingFile.FromDomainModel(model);
			else throw new FileRefNotEmptyException(model.FileRef);

			await _dbContext.SaveChangesAsync();
		}
	}
}
