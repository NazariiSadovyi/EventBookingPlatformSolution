namespace EBP.Domain.Exceptions
{
    public class BookingTicketsBookingConcurrencyException : DomainExceptionBase
    {
        public BookingTicketsBookingConcurrencyException(IEnumerable<Guid> bookingTicketIds)
            : base($"Concurrency exception occurred while booking booking tickets with ids: {string.Join(", ", bookingTicketIds)}")
        {
        }
    }
}
