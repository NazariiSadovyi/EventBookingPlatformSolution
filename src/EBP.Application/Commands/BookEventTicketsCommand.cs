using EBP.Domain.Enums;
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
        TicketKind Kind,
        int TicketCount);
}
