using EBP.Application.Interfaces;
using EBP.Domain.Repositories;

namespace EBP.Application.UseCases
{
    public class RefundBookingUseCase(
        IBookingRefundRepository _bookingRefundRepository,
        IDbSessionRepository _dbSessionRepository)
        : IBackgroundRequestHandler
    {
        public async Task HandleAsync(CancellationToken cancellationToken = default)
        {
            var bookingRefund = await _bookingRefundRepository.GetNextRefundAsync(cancellationToken);

            if (bookingRefund is null)
                return;

            // Simulate processing time for the refund.
            await Task.Delay(1000);

            bookingRefund.ProcessRefund();

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
