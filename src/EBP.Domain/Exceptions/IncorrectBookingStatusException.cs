using EBP.Domain.Enums;

namespace EBP.Domain.Exceptions
{
    public class IncorrectBookingStatusException : DomainExceptionBase
    {
        public IncorrectBookingStatusException(Guid bookingId, BookingStatus actual, BookingStatus expected)
            : base($"Booking with id {bookingId} has an incorrect status. Expected: {expected}, Actual: {actual}.")
        {
        }
    }
}
