using EBP.Domain.Entities;
using MediatR;

namespace EBP.Application.Commands
{
    public record class CreateBookingEventCommand(
        string? Name,
        string? Description,
        DateTime StartAt,
        TimeSpan Duration,
        int StandartTicketsCount,
        int VipTicketsCount,
        int StudentTicketsCount)
        : IRequest<BookingEvent>
    {
    }
}
