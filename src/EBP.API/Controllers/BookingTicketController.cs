using EBP.API.Models;
using EBP.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Route("bookingTickets")]
    public class BookingTicketController(ISender mediator) : ControllerBase
    {
        [HttpPost("book")]
        public async Task<IEnumerable<BookingTicketResponse>> Book(BookTicketRequest bookTicketRequest)
        {
            var command = new BookTicketsCommand(bookTicketRequest.BookingTicketIds);

            var result = await mediator.Send(command);

            var response = result.Select(_ => new BookingTicketResponse
            {
                Id = _.Id,
                TicketType = (TicketTypeContract)(int)_.Type,
            }).ToArray();

            return response;
        }
    }
}
