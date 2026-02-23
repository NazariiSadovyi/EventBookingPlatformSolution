using EBP.Application.DTOs;
using MediatR;

namespace EBP.Application.Queries
{
    public record class GetBookingStatisticsQuery()
        : IRequest<IEnumerable<BookingStatisticsDto>>
    {
    }
}