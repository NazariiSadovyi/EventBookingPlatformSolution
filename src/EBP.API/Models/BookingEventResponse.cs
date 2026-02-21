namespace EBP.API.Models
{
    public class BookingEventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desciption { get; set; }
        public DateTime StartAt { get; set; }
        public TimeSpan Duration { get; set; }
        public BookingTicketResponse[] Tickets { get; set; } = null!;
    }
}
