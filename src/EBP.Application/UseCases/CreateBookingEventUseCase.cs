using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class CreateBookingEventUseCase(
        TimeProvider _timeProvider,
        IBookingEventRepository bookingEventRepository,
        IDbSessionRepository dbSessionRepository)
        : IRequestHandler<CreateBookingEventCommand, BookingEvent>
    {
        public async Task<BookingEvent> Handle(CreateBookingEventCommand request, CancellationToken cancellationToken)
        {
            var newBookingEvent = BookingEvent.CreateNew(
                request.Title!,
                request.Description,
                request.StartAt,
                request.Duration,
                _timeProvider.GetUtcNow().DateTime);

            await bookingEventRepository.AddAsync(newBookingEvent, cancellationToken);

            await dbSessionRepository.SaveChangesAsync(cancellationToken);

            return newBookingEvent;
        }
    }
}
