using EBP.Application.Commands;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class ReleaseBookingUseCase(
        IDbSessionRepository _dbSessionRepository,
        IBookingRepository _bookingRepository)
        : IRequestHandler<ReleaseBookingCommand>
    {
        public async Task Handle(ReleaseBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetAsync(request.BookingId, cancellationToken);
            if (booking is null)
                throw new BookingNotFoundException(request.BookingId);

            booking.ReleaseBooking();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
