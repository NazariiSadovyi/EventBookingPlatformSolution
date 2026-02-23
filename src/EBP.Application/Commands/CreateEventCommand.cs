using EBP.Domain.Enums;
using MediatR;

namespace EBP.Application.Commands
{
    public record class CreateEventCommand(
        string? Name,
        string? Description,
        DateTime StartAt,
        TimeSpan Duration,
        IEnumerable<CreateEventTicketDetail> TicketDetails)
        : IRequest<Guid>
    {
    }

    public record CreateEventTicketDetail(
         TicketKind Kind,
         decimal Price,
         int Count)
    {
    }
}
