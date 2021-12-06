using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.Models.LoanApplication;
using Swashbuckle.AspNetCore.Annotations;

namespace Nordax.Bank.Recruitment.Controllers
{
    [ApiController]
    [Route("api/loan-application")]
    public class LoanApplicationController : ControllerBase
    {
        public LoanApplicationController()
        {
        }

        [HttpPost("attachment")]
        [SwaggerResponse(StatusCodes.Status200OK, "File uploaded successfully", typeof(FileResponse))]
        [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
	        //TODO: Store file
	        return Ok();
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