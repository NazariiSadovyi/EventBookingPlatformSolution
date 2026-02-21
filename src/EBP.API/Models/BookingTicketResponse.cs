namespace EBP.API.Models
{
    public class BookingTicketResponse
    {
        public Guid Id { get; set; }
        public TicketTypeContract TicketType { get; set; }
    }
}
