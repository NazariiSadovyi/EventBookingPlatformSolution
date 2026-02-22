using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Domain.ValueObjects;
using MediatR;
using System.Data;

namespace EBP.Application.UseCases
{
    internal class CreateBookingEventUseCase(
        ITimeProvider _timeProvider,
        IBookingEventRepository bookingEventRepository,
        IApplicationUserProvider applicationUserProvider)
        : IRequestHandler<CreateBookingEventCommand, BookingEvent>
    {
        public async Task<BookingEvent> Handle(CreateBookingEventCommand request, CancellationToken cancellationToken)
        {
            var newBookingEvent = BookingEvent.CreateNew(
                request.Name!,
                request.Description,
                request.StartAt,
                request.Duration,
                request.StandartTicketsCount,
                request.VipTicketsCount,
                request.StudentTicketsCount,
                _timeProvider.Now);

            var result = await bookingEventRepository.AddAsync(newBookingEvent, cancellationToken);
            if (result == BookingEventCreationResult.NameAlreadyExists)
                throw new BookingEventNameAlreadyExistsException(newBookingEvent.Name);

            return newBookingEvent;
        }
    }
}
