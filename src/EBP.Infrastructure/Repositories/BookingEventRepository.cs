using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Domain.ValueObjects;
using EBP.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Repositories
{
    internal class BookingEventRepository(
        ApplicationDbContext applicationDbContext,
        ITimeProvider timeProvider)
        : IBookingEventRepository
    {
        public async Task<IEnumerable<BookingEvent>> GetAvailableAsync(TicketType? ticketType = null, CancellationToken cancellationToken = default)
        {
            IQueryable<BookingEvent> query = applicationDbContext.BookingEvents;

            if (ticketType is not null)
                query = query.Where(_ => _.Tickets.Any(__ => __.Type == ticketType));

            return await query
                .Where(_ => _.StartAt > timeProvider.Now)
                .Include(_ => _.Tickets)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<BookingEvent?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.BookingEvents
                .Include(_ => _.Tickets)
                .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<BookingEventCreationResult> AddAsync(BookingEvent bookingEvent, CancellationToken cancellationToken = default)
        {
            try
            {
                await applicationDbContext.AddAsync(bookingEvent, cancellationToken);
                await applicationDbContext.SaveChangesAsync(cancellationToken);

                return BookingEventCreationResult.Success;
            }
            catch (DbUpdateException e) when (e.IsUniqueViolation(nameof(BookingEvent.Name)))
            {
                return BookingEventCreationResult.NameAlreadyExists;
            }
        }

        public void Remove(BookingEvent bookingEvent)
        {
            applicationDbContext.Remove(bookingEvent);
        }
    }
}
