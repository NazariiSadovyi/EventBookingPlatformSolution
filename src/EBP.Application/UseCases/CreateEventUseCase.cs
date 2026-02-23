using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Enums;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class CreateEventUseCase(
        ITimeProvider _timeProvider,
        IEventRepository _eventRepository)
        : IRequestHandler<CreateEventCommand, Guid>
    {
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = Event.CreateNew(
                request.Name!,
                request.Description,
                request.StartAt,
                request.Duration,
                request.TicketDetails.Select(_ => new ValueTuple<TicketKind, decimal, int>(_.Kind, _.Price, _.Count)).ToArray(),
                _timeProvider.Now);

            var result = await _eventRepository.AddAsync(newEvent, cancellationToken);
            if (result == EventCreationResult.NameAlreadyExists)
                throw new EventNameAlreadyExistsException(newEvent.Name);

            return newEvent.Id;
        }
    }
}
