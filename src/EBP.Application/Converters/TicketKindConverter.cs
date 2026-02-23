using EBP.Application.DTOs;
using EBP.Domain.Enums;

namespace EBP.Application.Converters
{
    public static class TicketKindConverter
    {
        public static TicketKindDto ToDto(this TicketKind ticketKind)
        {
            return ticketKind switch
            {
                TicketKind.Regular => TicketKindDto.Regular,
                TicketKind.VIP => TicketKindDto.VIP,
                TicketKind.Student => TicketKindDto.Student,
                _ => throw new ArgumentOutOfRangeException(nameof(ticketKind), $"Not expected ticket kind value: {ticketKind}"),
            };
        }

        public static TicketKind ToDomain(this TicketKindDto ticketKindDto)
        {
            return ticketKindDto switch
            {
                TicketKindDto.Regular => TicketKind.Regular,
                TicketKindDto.VIP => TicketKind.VIP,
                TicketKindDto.Student => TicketKind.Student,
                _ => throw new ArgumentOutOfRangeException(nameof(ticketKindDto), $"Not expected ticket kind DTO value: {ticketKindDto}"),
            };
        }

        public static TicketKind? ToDomain(this TicketKindDto? ticketKindDto)
        {
            return ticketKindDto switch
            {
                TicketKindDto.Regular => TicketKind.Regular,
                TicketKindDto.VIP => TicketKind.VIP,
                TicketKindDto.Student => TicketKind.Student,
                null => null,
                _ => throw new ArgumentOutOfRangeException(nameof(ticketKindDto), $"Not expected ticket kind DTO value: {ticketKindDto}"),
            };
        }
    }
}
