using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.ValueObjects;

namespace EBP.Domain.Repositories
{
    public interface IBookingEventRepository
    {
        public Task<IEnumerable<BookingEvent>> GetAvailableAsync(TicketType? ticketType = null, CancellationToken cancellationToken = default);
        public Task<BookingEvent?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<BookingEventCreationResult> AddAsync(BookingEvent bookingEvent, CancellationToken cancellationToken = default);
        public void Remove(BookingEvent bookingEvent);
    }
}
