using EBP.Application.Commands;
using EBP.Domain.Exceptions;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    public class RemoveEventTicketUseCase(
        IDbSessionRepository _dbSessionRepository,
        IEventRepository _eventRepository)
        : IRequestHandler<RemoveEventTicketCommand>
    {
        public async Task Handle(RemoveEventTicketCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);
            if (@event is null)
                throw new EventNotFoundException(request.EventId);

            @event.RemoveTicket(request.TicketId);

            await _dbSessionRepository.SaveChangesAsync();
        }
    }
}