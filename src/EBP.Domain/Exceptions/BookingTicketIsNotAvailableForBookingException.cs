namespace EBP.Domain.Exceptions
{
    public class BookingTicketIsNotAvailableForBookingException : DomainExceptionBase
    {
        public BookingTicketIsNotAvailableForBookingException(Guid ticketId)
            : base($"Booking ticket with id {ticketId} is not available for booking.")
        {
        }
    }
}
