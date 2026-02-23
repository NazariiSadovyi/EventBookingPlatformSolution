namespace EBP.Application.DTOs
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desciption { get; set; }
        public DateTime StartAt { get; set; }
        public TimeSpan Duration { get; set; }
        public EventTicketDetailsDto[] TicketDetails { get; set; } = null!;
    }
}