namespace EBP.API.Models
{
    public class BookingTicketResponse
    {
        public Guid Id { get; set; }
        public TicketTypeContract Type { get; set; }
    }
}
