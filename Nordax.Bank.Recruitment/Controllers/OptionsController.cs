using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Shared.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OptionsController : ControllerBase
	{
		private IOptionService _optionService;

		public OptionsController(IOptionService optionService) =>
			_optionService = optionService;

		[HttpGet("binding-periods")]
		[SwaggerResponse(StatusCodes.Status200OK, "Binding periods fetched successfully", typeof(IEnumerable<BindingPeriodModel>))]
		[SwaggerResponse(StatusCodes.Status204NoContent, "Binding periods do not exist")]
		[ProducesResponseType(typeof(IEnumerable<BindingPeriodModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> GetBindingPeriods()
		{
			try
			{
				var bindingPeriods = await _optionService.GetBindingPeriods();
				if (bindingPeriods.Count == 0)
					return NoContent();

				return Ok(bindingPeriods);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
