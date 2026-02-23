using EBP.API.Mappers;
using EBP.API.Models;
using EBP.Application.Commands;
using EBP.Application.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    [Route("api/admin")]
    public class AdminController(ISender _sender) : ControllerBase
    {
        [HttpPost("events")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest createEventRequest)
        {
            var command = new CreateEventCommand(
                createEventRequest.Name,
                createEventRequest.Description,
                createEventRequest.StartAt,
                createEventRequest.Duration,
                createEventRequest.TicketDetails.ToApplications());
            var eventId = await _sender.Send(command);
            return Ok(new { eventId });
        }
    }
}
