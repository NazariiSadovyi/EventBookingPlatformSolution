namespace EBP.Domain.Exceptions
{
    public class EventNameAlreadyExistsException : DomainExceptionBase
    {
        public EventNameAlreadyExistsException(string name)
            : base($"Failed to create booking event. Reason: Name '{name}' already exists.")
        {
        }
    }
}
