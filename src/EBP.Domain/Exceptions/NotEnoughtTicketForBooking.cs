namespace EBP.Domain.Exceptions
{
    public class NotEnoughtTicketForBooking : DomainExceptionBase
    {
        public NotEnoughtTicketForBooking()
            : base("Not enough tickets available for booking.")
        {
        }
    }
}
