using EBP.Domain.Entities;
using EBP.Domain.ValueObjects;

namespace EBP.Domain.Repositories
{
    public interface IBookingEventRepository
    {
        public Task<IEnumerable<BookingEvent>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<BookingEvent?> FindAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<BookingEventCreationResult> AddAsync(BookingEvent bookingEvent, CancellationToken cancellationToken = default);
        public void Remove(BookingEvent bookingEvent);
    }
}
