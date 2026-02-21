using EBP.Domain.Entities;

namespace EBP.Domain.Repositories
{
    public interface IBookingTicketRepository
    {
        public Task<IEnumerable<BookingTicket>> GetTicketsAsync(Guid[] ticketIds, CancellationToken cancellationToken = default);
        public Task<IEnumerable<BookingTicket>> GetExpiredBookedTicketsAsync(TimeSpan allowedBookedPeriod, CancellationToken cancellationToken = default);
    }
}
