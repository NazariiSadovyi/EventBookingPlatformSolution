using EBP.Application.DTOs;
using MediatR;

namespace EBP.Application.Commands
{
    public record class BookEventTicketsCommand(
        Guid EventId,
        IEnumerable<BookEventTicketsDetail> TicketDetails)
        : IRequest<Guid>
    {
    }

    public record class BookEventTicketsDetail(
        TicketKindDto Kind,
        int TicketCount);
}
