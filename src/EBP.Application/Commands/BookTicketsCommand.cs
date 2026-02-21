using EBP.Domain.Entities;
using MediatR;

namespace EBP.Application.Commands
{
    public record class BookTicketsCommand(
        Guid[] BookingTicketIds)
        : IRequest<IEnumerable<BookingTicket>>
    {
    }
}
