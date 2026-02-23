using EBP.Application.Commands;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class CancelBookingUseCase(
        IDbSessionRepository _dbSessionRepository,
        IBookingRepository _bookingRepository)
        : IRequestHandler<CancelBookingCommand>
    {
        public async Task Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetAsync(request.BookingId, cancellationToken);
            if (booking is null)
                throw new BookingNotFoundException(request.BookingId);

            booking.CancelBooking();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
