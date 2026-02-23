using EBP.Domain.Entities;

namespace EBP.Domain.Repositories
{
    public interface IBookingRefundRepository : ISessionRepository
    {
        Task AddAsync(BookingRefund bookingRefund, CancellationToken cancellationToken = default);
        Task<BookingRefund?> GetNextRefundAsync(CancellationToken cancellationToken = default);
    }
}
