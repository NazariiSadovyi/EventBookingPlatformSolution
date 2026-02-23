using EBP.Domain.Entities;

namespace EBP.Domain.Repositories
{
    public interface ITicketRepository : ISessionRepository
    {
        Task<IEnumerable<Ticket>> GetTicketsAsync(Guid[] ticketIds, CancellationToken cancellationToken = default);
    }
}
