using EBP.Application.Converters;
using EBP.Application.DTOs;
using EBP.Application.Queries;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class GetBookingHistoryUseCase(
        IBookingRepository _bookingRepository,
        IApplicationUserProvider _applicationUserProvider)
        : IRequestHandler<GetBookingHistoryQuery, IEnumerable<BookingHistoryDto>>
    {
        public async Task<IEnumerable<BookingHistoryDto>> Handle(GetBookingHistoryQuery request, CancellationToken cancellationToken)
        {
            var bookingHistory = await _bookingRepository.GetUsersBookingAsync(_applicationUserProvider.Current.UserId, cancellationToken);
            return bookingHistory.Select(_ => new BookingHistoryDto(_.Id, _.Event.Name, _.Status.ToDto()));
        }
    }
}
