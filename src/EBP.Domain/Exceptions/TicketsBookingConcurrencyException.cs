namespace EBP.Domain.Exceptions
{
    public class TicketsBookingConcurrencyException : DomainExceptionBase
    {
        public TicketsBookingConcurrencyException()
            : base($"Concurrency exception occurred while booking tickets. Please try again.")
        {
        }
    }
}
