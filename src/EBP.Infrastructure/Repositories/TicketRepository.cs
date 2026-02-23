using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EBP.Infrastructure.Repositories
{
    internal class TicketRepository(
        ApplicationDbContext applicationDbContext,
        ITimeProvider timeProvider,
        IOptions<ReleaseBookingOptions> options)
        : ITicketRepository
    {
        public async Task<IEnumerable<Ticket>> GetTicketsAsync(Guid[] ticketIds, CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.Tickets
                .Where(_ => ticketIds.Contains(_.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
