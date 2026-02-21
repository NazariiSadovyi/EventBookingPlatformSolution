using EBP.API.Models;
using EBP.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class BookingTicketMapper
    {
        public static partial BookingTicketResponse ToResponse(this BookingTicket _);
        public static partial IEnumerable<BookingTicketResponse> ToResponses(this IEnumerable<BookingTicket> _);
    }
}
