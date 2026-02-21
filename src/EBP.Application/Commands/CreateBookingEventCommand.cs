using EBP.Domain.Entities;
using MediatR;

namespace EBP.Application.Commands
{
    public record class CreateBookingEventCommand(
        string? Title,
        string? Description,
        DateTime StartAt,
        TimeSpan Duration)
        : IRequest<BookingEvent>
    {
    }
}
