using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Repositories
{
    internal class TicketRepository(
        ApplicationDbContext applicationDbContext)
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
