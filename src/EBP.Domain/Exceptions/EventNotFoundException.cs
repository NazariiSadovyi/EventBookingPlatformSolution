namespace EBP.Domain.Exceptions
{
    public class EventNotFoundException : DomainExceptionBase
    {
        public EventNotFoundException(Guid eventId)
            : base($"Event with id {eventId} not found.")
        {
        }
    }
}
