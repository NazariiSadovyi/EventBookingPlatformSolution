using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EBP.Infrastructure.Repositories
{
    internal class BookingRepository(
        ApplicationDbContext _applicationDbContext,
        ITimeProvider _timeProvider,
        IOptions<ReleaseBookingOptions> _options)
        : IBookingRepository
    {
        public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            await _applicationDbContext.Bookings.AddAsync(booking, cancellationToken);
        }

        public Task<Booking?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _applicationDbContext.Bookings
                .Include(_ => _.Tickets)
                .Include(_ => _.Event)
                .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public void Remove(Booking booking)
        {
            _applicationDbContext.Bookings.Remove(booking);
        }

        public async Task<IEnumerable<Booking>> GetExpiredBookingsAsync(CancellationToken cancellationToken = default)
        {
            var allowedBookedPeriod = _options.Value.AllowedExpirationBookedPeriod;
            return await _applicationDbContext.Bookings
                .Include(_ => _.Tickets)
                .Where(_ => _.Status == BookingStatus.Booked && _timeProvider.Now.Add(-allowedBookedPeriod) > _.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
