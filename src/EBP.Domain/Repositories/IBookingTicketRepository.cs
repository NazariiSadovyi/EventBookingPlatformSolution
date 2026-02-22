using EBP.Domain.Entities;

namespace EBP.Domain.Repositories
{
    public interface IBookingTicketRepository : ISessionRepository
    {
        Task<IEnumerable<BookingTicket>> GetTicketsAsync(Guid[] ticketIds, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookingTicket>> GetExpiredBookedTicketsAsync(CancellationToken cancellationToken = default);
    }
}
