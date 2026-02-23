using EBP.API.Models;
using EBP.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class TicketMapper
    {
        public static partial TicketResponse ToResponse(this Ticket _);
        public static partial IEnumerable<TicketResponse> ToResponses(this IEnumerable<Ticket> _);
    }
}
