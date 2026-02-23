using MediatR;

namespace EBP.Application.Commands
{
    public record class ReleaseBookingCommand(
        Guid BookingId)
        : IRequest
    {
    }
}
