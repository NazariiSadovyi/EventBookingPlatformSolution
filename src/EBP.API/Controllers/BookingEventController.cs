using EBP.API.Models;
using EBP.Application.Commands;
using EBP.Application.Queries;
using EBP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Route("bookingEvents")]
    public class BookingEventController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<BookingEventResponse>> Get(TicketTypeContract? ticketType)
        {
            var query = new GetBookingEventsQuery((TicketType?)(int?)ticketType);

            var result = await mediator.Send(query);

            var response = result.Select(_ => new BookingEventResponse
            {
                Id = _.Id,
                Name = _.Name,
                Desciption = _.Desciption,
                StartAt = _.StartAt,
                Duration = _.Duration,
                Tickets = _.Tickets.Select(t => new BookingTicketResponse
                {
                    Id = t.Id,
                    TicketType = (TicketTypeContract)(int)t.Type,
                }).ToArray()
            });

            return response;
        }

        [HttpPost]
        public async Task<BookingEventResponse> Create(CreateBookingEventRequest createBookingEvent)
        {
            var command = new CreateBookingEventCommand(
                createBookingEvent.Title,
                createBookingEvent.Description,
                createBookingEvent.StartAt,
                createBookingEvent.Duration,
                createBookingEvent.StandardTicketsCount,
                createBookingEvent.VipTicketsCount,
                createBookingEvent.StudentTicketsCount);

            var result = await mediator.Send(command);

            var response = new BookingEventResponse
            {
                Id = result.Id,
                Name = result.Name,
                Desciption = result.Desciption,
                StartAt = result.StartAt,
                Duration = result.Duration,
                Tickets = result.Tickets.Select(_ => new BookingTicketResponse
                {
                    Id = _.Id,
                    TicketType = (TicketTypeContract)(int)_.Type,
                }).ToArray()
            };

            return response;
        }
    }
}
