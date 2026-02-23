using EBP.Domain.Entities;

namespace EBP.Domain.Repositories
{
    public interface IBookingRepository : ISessionRepository
    {
        Task<Booking?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Booking booking, CancellationToken cancellationToken = default);
        void Remove(Booking booking);
        Task<IEnumerable<Booking>> GetExpiredBookingsAsync(CancellationToken cancellationToken = default);
    }
}
