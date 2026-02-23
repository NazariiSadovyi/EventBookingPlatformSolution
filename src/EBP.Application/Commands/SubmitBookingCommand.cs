using MediatR;

namespace EBP.Application.Commands
{
    public record class SubmitBookingCommand(
        Guid BookingId)
        : IRequest
    {
    }
}
