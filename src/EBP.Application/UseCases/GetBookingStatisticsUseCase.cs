using EBP.Application.DTOs;
using EBP.Application.Queries;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class GetBookingStatisticsUseCase(
        IBookingRepository _bookingRepository)
        : IRequestHandler<GetBookingStatisticsQuery, IEnumerable<BookingStatisticsDto>>
    {
        public async Task<IEnumerable<BookingStatisticsDto>> Handle(GetBookingStatisticsQuery request, CancellationToken cancellationToken)
        {
            var stats = await _bookingRepository.GetStatisticsAsync(cancellationToken);
            return stats.Select(s => new BookingStatisticsDto(
                s.EventId,
                s.EventName,
                s.TotalBookings,
                s.BookedCount,
                s.SubmittedCount,
                s.CancelledCount,
                s.ReleasedCount,
                s.ExpiredCount,
                s.UsedCount));
        }
    }
}