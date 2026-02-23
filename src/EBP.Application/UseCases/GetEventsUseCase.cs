using EBP.Application.Queries;
using EBP.Domain.Entities;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class GetEventsUseCase(
        IEventRepository eventRepository)
        : IRequestHandler<GetEventsQuery, IEnumerable<Event>>
    {
        public Task<IEnumerable<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return eventRepository.GetAvailableAsync(request.TicketType, cancellationToken);
        }
    }
}
