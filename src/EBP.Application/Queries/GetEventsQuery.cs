using EBP.Application.DTOs;
using MediatR;

namespace EBP.Application.Queries
{
    public record class GetEventsQuery(
        TicketKindDto? TicketType)
        : IRequest<IEnumerable<EventDto>>
    {
    }
}
