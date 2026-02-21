using EBP.Application.Queries;
using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class GetBookingEventsUseCase(
        IBookingEventRepository bookingEventRepository)
        : IRequestHandler<GetBookingEventsQuery, IEnumerable<BookingEvent>>
    {
        public Task<IEnumerable<BookingEvent>> Handle(GetBookingEventsQuery request, CancellationToken cancellationToken)
        {
            return bookingEventRepository.GetAvailableAsync(request.TicketType, cancellationToken);
        }
    }
}
