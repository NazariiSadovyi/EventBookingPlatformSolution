using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EBP.Infrastructure.Repositories
{
    internal class BookingTicketRepository(
        ApplicationDbContext applicationDbContext,
        ITimeProvider timeProvider,
        IOptions<ReleaseBookingOptions> options)
        : IBookingTicketRepository
    {
        public async Task<IEnumerable<BookingTicket>> GetTicketsAsync(Guid[] ticketIds, CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.BookingTickets
                .Where(_ => ticketIds.Contains(_.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BookingTicket>> GetExpiredBookedTicketsAsync(CancellationToken cancellationToken = default)
        {
            var allowedBookedPeriod = options.Value.AllowedExpirationBookedPeriod;
            return await applicationDbContext.BookingTickets
                .Where(_ => _.Status == TicketStatus.Booked
                    && timeProvider.Now.Add(-allowedBookedPeriod) > _.BookedAt!.Value )
                .ToListAsync(cancellationToken);
        }
    }
}
