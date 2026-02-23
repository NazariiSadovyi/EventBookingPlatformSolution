using EBP.API.Mappers;
using EBP.API.Models;
using EBP.Application.Commands;
using EBP.Application.Constants;
using EBP.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    [Route("bookingEvents")]
    public class BookingEventController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<BookingEventResponse>> Get(TicketTypeContract? ticketType)
        {
            var query = new GetBookingEventsQuery(ticketType.ToDomain());
            var result = await mediator.Send(query);
            return result.ToResponses();
        }

        [HttpPost]
        public async Task<BookingEventResponse> Create(CreateBookingEventRequest createRequest)
        {
            var command = new CreateBookingEventCommand(
                createRequest.Title,
                createRequest.Description,
                createRequest.StartAt,
                createRequest.Duration,
                createRequest.StandardTicketsCount,
                createRequest.VipTicketsCount,
                createRequest.StudentTicketsCount);
            var result = await mediator.Send(command);
            return result.ToResponse();
        }
    }
}
