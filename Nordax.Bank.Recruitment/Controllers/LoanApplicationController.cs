using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.Domain.Providers;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Helpers;
using Nordax.Bank.Recruitment.Models.LoanApplication;
using Swashbuckle.AspNetCore.Annotations;

namespace Nordax.Bank.Recruitment.Controllers
{
	[ApiController]
	[Route("api/loan-application")]
	public class LoanApplicationController : ControllerBase
	{
		private ILoanApplicationService _loanApplicationService;
		private IFileStoreProvider _fileStoreProvider;
		private IFileUploadHelper _fileUploadHelper;

		public LoanApplicationController(
			ILoanApplicationService loanApplicationService,
			IFileStoreProvider fileStoreProvider,
			IFileUploadHelper fileUploadHelper)
		{
			_loanApplicationService = loanApplicationService;
			_fileStoreProvider = fileStoreProvider;
			_fileUploadHelper = fileUploadHelper;
		}

		[HttpPost("attachment/{documentType}")]
		[SwaggerResponse(StatusCodes.Status200OK, "File uploaded successfully", typeof(FileResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "File does not fullfil requirement", typeof(FileResponse))]
		[ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(FileResponse), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UploadFile(string documentType, IFormFile file)
		{
			if (!_fileUploadHelper.ValidateRequirement(file, out var reason))
				return BadRequest(new FileResponse { ErrorMessage = reason });

			byte[] content;
			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				content = stream.ToArray();
			}

			var fileExtension = Path.GetExtension(file.FileName);
			if (!_fileUploadHelper.VerifySignature(fileExtension, content))
				return BadRequest(new FileResponse { ErrorMessage = "File signature does not match the extension." });

			var fileRef = await _fileStoreProvider.SaveFile(documentType, file.FileName, content);

			return Ok(new FileResponse { FileRef = fileRef });
		}

		[HttpPost]
		[SwaggerResponse(StatusCodes.Status200OK, "Loan Application registered successfully")]
		public async Task<IActionResult> RegisterLoanApplication([Required][FromBody] RegisterLoanApplicationRequest request)
		{
			//TODO: Store Loan Application
			return Ok();
		}

		[HttpGet("{fileId:Guid}")]
		[SwaggerResponse(StatusCodes.Status200OK, "Loan Application fetched successfully", typeof(LoanApplicationResponse))]
		[ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetLoanApplication([FromRoute] Guid fileId)
		{
			//TODO: Get Loan Application
			return Ok();
		}

		[HttpGet]
		[SwaggerResponse(StatusCodes.Status200OK, "Loan Applications fetched successfully", typeof(IEnumerable<LoanApplicationResponse>))]
		[SwaggerResponse(StatusCodes.Status204NoContent, "Loan Applications do not exist")]
		[ProducesResponseType(typeof(IEnumerable<LoanApplicationResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetLoanApplications()
		{
			var loanApplications = await _loanApplicationService.GetAllLoanApplications();
			if (loanApplications.Count == 0)
				return NoContent();

			return Ok(loanApplications.Select(la => new LoanApplicationResponse
			{
				Id = la.Id,
				CaseNo = la.CaseNo,
				CurrentStep = la.CurrentStep,
				CreatedDate = la.CreatedDate,
				ApplicantOrganizationNo = la.Applicant.OrganizationNo,
				ApplicantFullName = $"{la.Applicant.FirstName} {la.Applicant.Surname}",
				LoanAmount = la.Loan.Amount,
				LoanBindingPeriod = la.Loan.BindingPeriod,
				LoanInterestRate = la.Loan.InterestRate,
				Documents = la.Documents.Select(d => new LoanApplicationDocumentResponse
				{
					Id = d.Id,
					DocumentType = d.DocumentType
				}).ToList()
			}));
		}
	}
}