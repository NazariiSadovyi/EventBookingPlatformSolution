using EBP.Domain.Entities;
using EBP.Domain.Enums;

namespace EBP.Domain.Repositories
{
    public interface IEventRepository : ISessionRepository
    {
        Task<IEnumerable<Event>> GetAvailableAsync(TicketKind? ticketKind = null, CancellationToken cancellationToken = default);
        Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<EventCreationResult> AddAsync(Event @event, CancellationToken cancellationToken = default);
        void Remove(Event @event);
    }
}
