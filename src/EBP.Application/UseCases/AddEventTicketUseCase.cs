using EBP.Application.Commands;
using EBP.Application.Converters;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using MediatR;
using System.Linq;

namespace EBP.Application.UseCases
{
    internal class AddEventTicketUseCase(
        IDbSessionRepository _dbSessionRepository,
        IEventRepository _eventRepository)
        : IRequestHandler<AddEventTicketCommand, Guid>
    {
        public async Task<Guid> Handle(AddEventTicketCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);
            if (@event is null)
                throw new EventNotFoundException(request.EventId);

            var newTicket = @event.AddTicket(request.Kind.ToDomain(), request.Price);

            await _dbSessionRepository.SaveChangesAsync<IEventRepository>(_ => Task.CompletedTask, cancellationToken);

            return newTicket.Id;
        }
    }
}