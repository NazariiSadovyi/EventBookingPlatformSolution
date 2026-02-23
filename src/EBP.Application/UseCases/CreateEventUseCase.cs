using EBP.Application.Commands;
using EBP.Domain.Entities;
using EBP.Domain.Exceptions;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Domain.ValueObjects;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class CreateEventUseCase(
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
                request.StandartTicketsCount,
                request.VipTicketsCount,
                request.StudentTicketsCount,
                _timeProvider.Now);

            var result = await _eventRepository.AddAsync(newEvent, cancellationToken);
            if (result == EventCreationResult.NameAlreadyExists)
                throw new EventNameAlreadyExistsException(newEvent.Name);

            return newEvent.Id;
        }
    }
}
