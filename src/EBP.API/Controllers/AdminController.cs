using EBP.API.Mappers;
using EBP.API.Models;
using EBP.Application.Commands;
using EBP.Application.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EBP.Application.Queries;

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

        [HttpPost("events/{eventId}/tickets")]
        public async Task<IActionResult> AddTicket([FromRoute] Guid eventId, [FromBody] AddTicketRequest addTicketRequest)
        {
            var command = new AddEventTicketCommand(
                eventId,
                addTicketRequest.Kind,
                addTicketRequest.Price);
            var ticketId = await _sender.Send(command);
            return Ok(new { ticketId });
        }

        [HttpDelete("events/{eventId}/tickets/{ticketId}")]
        public async Task<IActionResult> RemoveTicket([FromRoute] Guid eventId, [FromRoute] Guid ticketId)
        {
            var command = new RemoveEventTicketCommand(eventId, ticketId);
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("booking/statistics")]
        public async Task<IActionResult> Statistics()
        {
            var query = new GetBookingStatisticsQuery();
            var result = await _sender.Send(query);
            return Ok(result);
        }
    }
}
