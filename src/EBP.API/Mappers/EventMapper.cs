using EBP.API.Models;
using EBP.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class EventMapper
    {
        public static partial EventResponse ToResponse(this Event _);
        public static partial IEnumerable<EventResponse> ToResponses(this IEnumerable<Event> _);
    }
}
