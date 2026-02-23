namespace EBP.Domain.Exceptions
{
    public class TicketIsNotAvailableForBookingException : DomainExceptionBase
    {
        public TicketIsNotAvailableForBookingException(Guid ticketId)
            : base($"Booking ticket with id {ticketId} is not available for booking.")
        {
        }
    }
}
