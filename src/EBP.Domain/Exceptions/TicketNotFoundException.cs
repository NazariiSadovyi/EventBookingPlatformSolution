namespace EBP.Domain.Exceptions
{
    public class TicketNotFoundException : DomainExceptionBase
    {
        public TicketNotFoundException(Guid ticketId)
            : base($"Ticket with id {ticketId} not found.")
        {
        }
    }
}