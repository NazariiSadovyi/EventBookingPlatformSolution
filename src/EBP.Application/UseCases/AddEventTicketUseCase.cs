using EBP.Application.Commands;
using EBP.Application.Converters;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class AddEventTicketUseCase(
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

            await _dbSessionRepository.SaveChangesAsync(cancellationToken);

            return newTicket.Id;
        }
    }
}