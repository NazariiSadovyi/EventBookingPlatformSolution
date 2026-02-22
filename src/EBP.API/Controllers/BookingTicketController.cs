using EBP.API.Mappers;
using EBP.API.Models;
using EBP.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("bookingTickets")]
    public class BookingTicketController(ISender mediator) : ControllerBase
    {
        [HttpPost("book")]
        public async Task<IEnumerable<BookingTicketResponse>> Book(BookTicketRequest bookTicketRequest)
        {
            var command = new BookTicketsCommand(bookTicketRequest.BookingTicketIds);
            var result = await mediator.Send(command);
            return result.ToResponses();
        }
    }
}
