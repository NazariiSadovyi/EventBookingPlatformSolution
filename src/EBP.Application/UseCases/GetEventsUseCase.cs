using EBP.Application.Converters;
using EBP.Application.DTOs;
using EBP.Application.Queries;
using EBP.Domain.Repositories;
using MediatR;

namespace EBP.Application.UseCases
{
    internal class GetEventsUseCase(
        IEventRepository eventRepository)
        : IRequestHandler<GetEventsQuery, IEnumerable<EventDto>>
    {
        public async Task<IEnumerable<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await eventRepository.GetAvailableAsync(request.TicketType.ToDomain(), cancellationToken);
            return events.ToDtos();
        }
    }
}
