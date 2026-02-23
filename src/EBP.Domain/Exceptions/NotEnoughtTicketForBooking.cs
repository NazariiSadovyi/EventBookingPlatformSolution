using EBP.Domain.Enums;

namespace EBP.Domain.Exceptions
{
    public class NotEnoughtTicketForBooking : DomainExceptionBase
    {
        public NotEnoughtTicketForBooking(TicketKind ticketKind)
            : base($"Not enough {ticketKind} tickets available for booking.")
        {
        }
    }
}
