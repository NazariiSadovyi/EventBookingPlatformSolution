using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.ValueObjects;

namespace EBP.Domain.Repositories
{
    public interface IBookingEventRepository : ISessionRepository
    {
        Task<IEnumerable<BookingEvent>> GetAvailableAsync(TicketType? ticketType = null, CancellationToken cancellationToken = default);
        Task<BookingEvent?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookingEventCreationResult> AddAsync(BookingEvent bookingEvent, CancellationToken cancellationToken = default);
        void Remove(BookingEvent bookingEvent);
    }
}
