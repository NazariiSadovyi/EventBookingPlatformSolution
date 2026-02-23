using MediatR;

namespace EBP.Application.Commands
{
    public record class CancelBookingCommand(
        Guid BookingId)
        : IRequest
    {
    }
}
