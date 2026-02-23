using EBP.API.Models;
using EBP.Application.Commands;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class CreateEventTicketDetailMapper
    {
        public static partial IEnumerable<CreateEventTicketDetail> ToApplications(this IEnumerable<CreateEventTicketDetailRequest> _);
    }
}
