using EBP.API.Models;
using EBP.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace EBP.API.Mappers
{
    [Mapper]
    public static partial class TicketKindMapper
    {
        public static partial TicketKindContract? ToContract(this TicketKind? _);
        public static partial TicketKind? ToDomain(this TicketKindContract? _);
        public static partial TicketKindContract ToContract(this TicketKind _);
        public static partial TicketKind ToDomain(this TicketKindContract _);
    }
}
