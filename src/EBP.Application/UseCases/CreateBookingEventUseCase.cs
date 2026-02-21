using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using EBP.Domain.ValueObjects;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class CreateBookingEventUseCase(
        TimeProvider _timeProvider,
        IBookingEventRepository bookingEventRepository)
        : IRequestHandler<CreateBookingEventCommand, BookingEvent>
    {
        public async Task<BookingEvent> Handle(CreateBookingEventCommand request, CancellationToken cancellationToken)
        {
            var newBookingEvent = BookingEvent.CreateNew(
                request.Name!,
                request.Description,
                request.StartAt,
                request.Duration,
                _timeProvider.GetUtcNow().DateTime);

            var result = await bookingEventRepository.AddAsync(newBookingEvent, cancellationToken);
            if (result == BookingEventCreationResult.NameAlreadyExists)
                throw new BookingEventNameAlreadyExistsException(newBookingEvent.Name);

            return newBookingEvent;
        }
    }
}
