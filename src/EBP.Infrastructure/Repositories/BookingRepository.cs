using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Models;
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
        public async Task<IEnumerable<Booking>> GetUsersBookingAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Bookings
                .Include(_ => _.Event)
                .Where(_ => _.UserId == userId)
                .ToListAsync(cancellationToken);
        }

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

        public async Task<IEnumerable<BookingStatistics>> GetStatisticsAsync(CancellationToken cancellationToken = default)
        {
            var grouped = await _applicationDbContext.Bookings
                .Include(b => b.Event)
                .GroupBy(b => new { b.Event.Id, b.Event.Name })
                .Select(g => new
                {
                    EventId = g.Key.Id,
                    EventName = g.Key.Name,
                    Total = g.Count(),
                    Booked = g.Count(b => b.Status == BookingStatus.Booked),
                    Submitted = g.Count(b => b.Status == BookingStatus.Submitted),
                    Cancelled = g.Count(b => b.Status == BookingStatus.Cancelled),
                    Released = g.Count(b => b.Status == BookingStatus.Released),
                    Expired = g.Count(b => b.Status == BookingStatus.Expired),
                    Used = g.Count(b => b.Status == BookingStatus.Used)
                })
                .ToListAsync(cancellationToken);

            return grouped.Select(g => new BookingStatistics(
                g.EventId,
                g.EventName,
                g.Total,
                g.Booked,
                g.Submitted,
                g.Cancelled,
                g.Released,
                g.Expired,
                g.Used)).ToArray();
        }
    }
}
