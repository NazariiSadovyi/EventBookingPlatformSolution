using EBP.API.Models;
using EBP.Application.Commands;
using EBP.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingEventController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<BookingEvent> Create(CreateBookingEvent createBookingEvent)
        {
            var command = new CreateBookingEventCommand(
                createBookingEvent.Title,
                createBookingEvent.Description,
                createBookingEvent.StartAt,
                createBookingEvent.Duration);

            var result = await mediator.Send(command);

            return result;
        }
    }
}
