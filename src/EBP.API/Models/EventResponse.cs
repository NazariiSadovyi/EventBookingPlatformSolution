namespace EBP.API.Models
{
    public class EventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desciption { get; set; }
        public DateTime StartAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
