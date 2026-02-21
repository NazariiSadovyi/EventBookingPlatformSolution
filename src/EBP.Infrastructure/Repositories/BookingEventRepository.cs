using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Repositories
{
    public class BookingEventRepository(ApplicationDbContext applicationDbContext) : IBookingEventRepository
    {
        public async Task AddAsync(BookingEvent bookingEvent, CancellationToken cancellationToken = default)
        {
            await applicationDbContext.AddAsync(bookingEvent, cancellationToken);
        }

        public async Task<IEnumerable<BookingEvent>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.BookingEvents.ToListAsync(cancellationToken);
        }

        public async Task<BookingEvent?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await applicationDbContext.BookingEvents.FindAsync([id], cancellationToken);
        }

        public void Remove(BookingEvent bookingEvent)
        {
            applicationDbContext.Remove(bookingEvent);
        }
    }
}
