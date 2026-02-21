using EBP.Domain.Entities;
using EBP.Domain.Enums;
using MediatR;

namespace EBP.Application.Queries
{
    public record class GetBookingEventsQuery(
        TicketType? TicketType)
        : IRequest<IEnumerable<BookingEvent>>
    {
    }
}
