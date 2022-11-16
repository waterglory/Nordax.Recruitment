using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.Domain.Providers;
using Nordax.Bank.Recruitment.Helpers;
using Nordax.Bank.Recruitment.Models.LoanApplication;
using Swashbuckle.AspNetCore.Annotations;

namespace Nordax.Bank.Recruitment.Controllers
{
    [ApiController]
    [Route("api/loan-application")]
    public class LoanApplicationController : ControllerBase
    {
        private IFileStoreProvider _fileStoreProvider;
        private IFileUploadHelper _fileUploadHelper;

        public LoanApplicationController(
            IFileStoreProvider fileStoreProvider,
            IFileUploadHelper fileUploadHelper)
        {
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
        public async Task<IActionResult> RegisterLoanApplication([Required] [FromBody] RegisterLoanApplicationRequest request)
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

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application fetched successfully", typeof(LoanApplicationResponse))]
        [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLoanApplications()
        {
            //TODO: Get Loan Applications
            return Ok();
        }
	}
}