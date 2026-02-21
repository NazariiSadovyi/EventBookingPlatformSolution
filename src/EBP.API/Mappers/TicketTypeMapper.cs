using EBP.API.Models;
using EBP.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class TicketTypeMapper
    {
        public static partial TicketTypeContract? ToContract(this TicketType? _);
        public static partial TicketType? ToDomain(this TicketTypeContract? _);
        public static partial TicketTypeContract ToContract(this TicketType _);
        public static partial TicketType ToDomain(this TicketTypeContract _);
    }
}
