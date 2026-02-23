using EBP.API.Models;
using EBP.Application.Commands;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class BookTicketsDetailRequestMapper
    {
        public static partial IEnumerable<BookEventTicketsDetail> ToApplications(this IEnumerable<BookTicketsDetailRequest> _);
    }
}
