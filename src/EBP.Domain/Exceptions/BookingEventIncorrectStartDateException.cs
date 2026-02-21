namespace EBP.Domain.Exceptions
{
    public class BookingEventIncorrectStartDateException : Exception
    {
        public BookingEventIncorrectStartDateException(DateTime startAt, DateTime now)
            : base($"Start time '{startAt}' must be in the future. Current time is '{now}'.")
        {
        }
    }
}
