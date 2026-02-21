using EBP.API.Models;
using EBP.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class BookingEventMapper
    {
        public static partial BookingEventResponse ToResponse(this BookingEvent _);
        public static partial IEnumerable<BookingEventResponse> ToResponses(this IEnumerable<BookingEvent> _);
    }
}
