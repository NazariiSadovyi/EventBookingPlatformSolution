using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Domain.ValueObjects;
using EBP.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Repositories
{
    internal class EventRepository(
        ApplicationDbContext applicationDbContext,
        ITimeProvider timeProvider)
        : IEventRepository
    {
        public async Task<IEnumerable<Event>> GetAvailableAsync(TicketType? ticketType = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Event> query = applicationDbContext.Events;

            if (ticketType is not null)
                query = query.Where(_ => _.Tickets.Any(__ => __.Type == ticketType));

            return await query
                .Where(_ => _.StartAt > timeProvider.Now)
                .Include(_ => _.Tickets)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.Events
                .Include(_ => _.Tickets)
                .ThenInclude(_ => _.Booking)
                .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<EventCreationResult> AddAsync(Event @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await applicationDbContext.AddAsync(@event, cancellationToken);
                await applicationDbContext.SaveChangesAsync(cancellationToken);

                return EventCreationResult.Success;
            }
            catch (DbUpdateException e) when (e.IsUniqueViolation(nameof(Event.Name)))
            {
                return EventCreationResult.NameAlreadyExists;
            }
        }

        public void Remove(Event @event)
        {
            applicationDbContext.Remove(@event);
        }
    }
}
