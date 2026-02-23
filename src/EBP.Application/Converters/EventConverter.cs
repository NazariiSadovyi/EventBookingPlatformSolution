using EBP.Application.DTOs;
using EBP.Domain.Entities;

namespace EBP.Application.Converters
{
    public static class EventConverter
    {
        public static IEnumerable<EventDto> ToDtos(this IEnumerable<Event> _)
        {
            return _.Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Desciption = e.Desciption,
                StartAt = e.StartAt,
                Duration = e.Duration,
                TicketDetails = e.Tickets
                    .GroupBy(_ => _.Type.Kind)
                    .Select(_ => new EventTicketDetailsDto
                    {
                        Kind = _.Key.ToDto(),
                        Price = _.First().Type.Price,
                        AvailableCount = _.Count(t => t.IsAvailable),
                        BookedCount = _.Count(t => !t.IsAvailable)
                    })
                    .ToArray()
            });
        }
    }
}
