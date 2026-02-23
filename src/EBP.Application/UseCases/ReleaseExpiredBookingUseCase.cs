using EBP.Application.Interfaces;
using EBP.Domain.Repositories;

namespace EBP.Application.UseCases
{
    public class ReleaseExpiredBookingUseCase(
        IBookingRepository _bookingRepository,
        IDbSessionRepository _dbSessionRepository)
        : IBackgroundRequestHandler
    {
        public async Task HandleAsync(CancellationToken cancellationToken = default)
        {
            var expiredBookings = await _bookingRepository.GetExpiredBookingsAsync(cancellationToken);

            foreach (var expiredBooking in expiredBookings)
                expiredBooking.ExpiryBooking();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
