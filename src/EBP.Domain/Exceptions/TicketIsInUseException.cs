namespace EBP.Domain.Exceptions
{
    public class TicketIsInUseException : DomainExceptionBase
    {
        public TicketIsInUseException(Guid ticketId)
            : base($"Ticket with id {ticketId} is in use and cannot be removed.")
        {
        }
    }
}