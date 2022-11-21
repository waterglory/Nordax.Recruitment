using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.File;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.DataAccess.Tests.Configuration;
using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.RepositoryTests
{
	[TestClass]
	public class FileRepositoryTests
	{
		private readonly IFileRepository _fileRepository;
		private readonly FileDbContext _testDbContext;

		public FileRepositoryTests()
		{
			var dbContextFactoryMock = new Mock<IFileDbContextFactory>();

			_testDbContext = FileEfConfig.CreateInMemoryTestDbContext();
			dbContextFactoryMock.Setup(d => d.Create()).Returns(FileEfConfig.CreateInMemoryFileDbContext());

			_fileRepository = new FileRepository(dbContextFactoryMock.Object);
		}

		private string GenerateFileRef() => $"test/{Guid.NewGuid()}";

		private FileRecord NewFileRecord() => new FileRecord
		{
			FileRef = GenerateFileRef(),
			Content = new byte[0],
			ContentType = "test/file",
			FileName = "testFileName",
			FileType = "tst"
		};

		private FileModel NewFileModel() => new FileModel
		{
			FileRef = GenerateFileRef(),
			Content = new byte[0],
			ContentType = "test/file",
			FileName = "testFileName",
			FileType = "tst"
		};

		private void CompareExpectedActual(FileModel expected, FileRecord actual)
		{
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.FileRef, actual.FileRef);
			Assert.AreEqual(expected.Content.Length, actual.Content.Length);
			Assert.AreEqual(expected.ContentType, actual.ContentType);
			Assert.AreEqual(expected.FileName, actual.FileName);
			Assert.AreEqual(expected.FileType, actual.FileType);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_testDbContext.Database.EnsureDeleted();
		}

		[TestMethod]
		public async Task GetFile_FileNotExists_ShouldThrowException()
		{
			var fileRef = GenerateFileRef();
			await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => _fileRepository.GetFile(fileRef));
		}

		[TestMethod]
		public async Task GetFile_FileExists_ShouldReturnCorrectFile()
		{
			var fileRec = NewFileRecord();
			_testDbContext.FileRecords.Add(NewFileRecord());
			_testDbContext.FileRecords.Add(fileRec);
			_testDbContext.FileRecords.Add(NewFileRecord());
			await _testDbContext.SaveChangesAsync();

			var fileModel = await _fileRepository.GetFile(fileRec.FileRef);

			Assert.IsNotNull(fileModel);
			Assert.AreEqual(fileRec.FileRef, fileModel.FileRef);
			Assert.AreEqual(fileRec.Content.Length, fileModel.Content.Length);
			Assert.AreEqual(fileRec.ContentType, fileModel.ContentType);
			Assert.AreEqual(fileRec.FileName, fileModel.FileName);
			Assert.AreEqual(fileRec.FileType, fileModel.FileType);
		}

		[TestMethod]
		public async Task SaveFile_NullArgument_ShouldThrowException()
		{
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _fileRepository.SaveFile(null));
		}

		[TestMethod]
		public async Task SaveFile_NewFile_ShouldAddFile()
		{
			var fileModel = NewFileModel();
			await _fileRepository.SaveFile(fileModel);

			var fileRec = await _testDbContext.FileRecords.SingleOrDefaultAsync();

			Assert.IsNotNull(fileRec);
			CompareExpectedActual(fileModel, fileRec);
		}

		[TestMethod]
		public async Task SaveFile_FileExists_ShouldThrowException()
		{
			var fileRec = NewFileRecord();
			_testDbContext.FileRecords.Add(fileRec);
			await _testDbContext.SaveChangesAsync();

			await Assert.ThrowsExceptionAsync<FileRefNotEmptyException>(() => _fileRepository.SaveFile(fileRec.ToDomainModel()));
		}

		[TestMethod]
		public async Task SaveFile_FileExistsForced_ShouldReplaceFile()
		{
			var fileRec = NewFileRecord();
			_testDbContext.FileRecords.Add(fileRec);
			await _testDbContext.SaveChangesAsync();

			var replacementFileModel = new FileModel
			{
				FileRef = fileRec.FileRef,
				Content = new byte[5],
				ContentType = "test/replaced-file",
				FileName = "testReplacedFileName",
				FileType = "tst"
			};
			await _fileRepository.SaveFile(replacementFileModel, force: true);

			var replacedFileRec = await _testDbContext.FileRecords.SingleOrDefaultAsync();

			Assert.IsNotNull(fileRec);
			CompareExpectedActual(replacementFileModel, replacedFileRec);
		}
	}
}
