using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Models.Subscriber;
using Swashbuckle.AspNetCore.Annotations;

namespace Nordax.Bank.Recruitment.Controllers
{
    [ApiController]
    [Route("api/subscriber")]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscribersController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Subscription registered successfully")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Email address already registered")]
        public async Task<IActionResult> AddSubscriber([Required] [FromBody] NewSubscriberRequest request)
        {
            try
            {
                var subscriberId = await _subscriptionService.RegisterSubscriptionAsync(request.Name, request.Email);
                return Ok(new NewSubscriberResponse(subscriberId));
            }
            catch (Exception e)
            {
                if (e is EmailAlreadyRegisteredException) return Conflict($"Email {request.Email} already registered");
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{subscriberId:Guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Unsubscribed successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> DeleteSubscriber([Required] [FromRoute] Guid subscriberId)
        {
            try
            {
                await _subscriptionService.DeleteSubscriberAsync(subscriberId);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is UserNotFoundException) return NotFound("No user found with that id");
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{subscriberId:Guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "User fetched successfully", typeof(SubscriberResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [ProducesResponseType(typeof(SubscriberResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubscriber([Required] [FromRoute] Guid subscriberId)
        {
            try
            {
                var subscriber = await _subscriptionService.GetSubscriberAsync(subscriberId);
                return Ok(new SubscriberResponse(subscriber));
            }
            catch (Exception e)
            {
                if (e is UserNotFoundException) return NotFound("No user found with that id");
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}