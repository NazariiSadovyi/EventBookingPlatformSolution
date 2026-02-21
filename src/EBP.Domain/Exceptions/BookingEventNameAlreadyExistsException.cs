namespace EBP.Domain.Exceptions
{
    public class BookingEventNameAlreadyExistsException : DomainExceptionBase
    {
        public BookingEventNameAlreadyExistsException(string name)
            : base($"Failed to create booking event. Reason: Name '{name}' already exists.")
        {
        }
    }
}
