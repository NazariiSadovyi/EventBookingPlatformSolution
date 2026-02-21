using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using EBP.Domain.ValueObjects;
using EBP.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Repositories
{
    public class BookingEventRepository(ApplicationDbContext applicationDbContext) : IBookingEventRepository
    {

        public async Task<IEnumerable<BookingEvent>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.BookingEvents.ToListAsync(cancellationToken);
        }

        public async Task<BookingEvent?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.BookingEvents.FindAsync([id], cancellationToken);
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
