using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class CancelBookingUseCase(
        IDbSessionRepository _dbSessionRepository,
        IBookingRepository _bookingRepository,
        IBookingRefundRepository _bookingRefundRepository,
        ITimeProvider timeProvider)
        : IRequestHandler<CancelBookingCommand>
    {
        private static readonly TimeSpan _allowedRefundTimeBeforeEvent = TimeSpan.FromDays(1);

        public async Task Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetAsync(request.BookingId, cancellationToken);
            if (booking is null)
                throw new BookingNotFoundException(request.BookingId);

            if (timeProvider.Now.Add(_allowedRefundTimeBeforeEvent) < booking.Event.StartAt)
            {
                var bookingRefund = BookingRefund.CreateNew(booking, booking.UserId);
                await _bookingRefundRepository.AddAsync(bookingRefund, cancellationToken);
            }

            booking.CancelBooking();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
