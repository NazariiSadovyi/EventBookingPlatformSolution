using EBP.API.Models;
using EBP.Application.Commands;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class CreateEventTicketDetailMapper
    {
        public static partial CreateEventTicketDetail ToDomain(this CreateEventTicketDetailRequest _);
        public static partial IEnumerable<CreateEventTicketDetail> ToDomains(this IEnumerable<CreateEventTicketDetailRequest> _);
    }
}
