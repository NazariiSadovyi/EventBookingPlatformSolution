namespace EBP.API.Models
{
    public class CreateBookingEvent
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
