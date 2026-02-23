using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Repositories
{
    internal class BookingRefundRepository(
        ApplicationDbContext applicationDbContext)
        : IBookingRefundRepository
    {
        public async Task AddAsync(BookingRefund bookingRefund, CancellationToken cancellationToken = default)
        {
            await applicationDbContext.BookingRefunds.AddAsync(bookingRefund, cancellationToken);
        }

        public Task<BookingRefund?> GetNextRefundAsync(CancellationToken cancellationToken = default)
        {
            return applicationDbContext.BookingRefunds
                .Where(_ => !_.IsRefunded)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
