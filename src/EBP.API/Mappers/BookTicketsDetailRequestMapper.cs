using EBP.API.Models;
using EBP.Application.Commands;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class BookTicketsDetailRequestMapper
    {
        public static partial IEnumerable<BookEventTicketsDetail> ToDomains(this IEnumerable<BookTicketsDetailRequest> _);
    }
}
