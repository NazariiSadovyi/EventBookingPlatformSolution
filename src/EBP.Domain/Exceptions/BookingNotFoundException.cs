namespace EBP.Domain.Exceptions
{
    public class BookingNotFoundException : DomainExceptionBase
    {
        public BookingNotFoundException(Guid bookingId)
            : base($"Booking with id {bookingId} not found.")
        {
        }
    }
}
