using EBP.Application.Commands;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class SubmitBookingUseCase(
        IDbSessionRepository _dbSessionRepository,
        IBookingRepository _bookingRepository)
        : IRequestHandler<SubmitBookingCommand>
    {
        public async Task Handle(SubmitBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetAsync(request.BookingId, cancellationToken);
            if (booking is null)
                throw new BookingNotFoundException(request.BookingId);

            booking.SubmitBooking();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
