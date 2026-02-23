using EBP.Domain.Entities;
using EBP.Domain.Enums;
using MediatR;

namespace EBP.Application.Queries
{
    public record class GetEventsQuery(
        TicketKind? TicketType)
        : IRequest<IEnumerable<Event>>
    {
    }
}
