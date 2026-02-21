namespace EBP.API.Models
{
    public class CreateBookingEventRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartAt { get; set; }
        public TimeSpan Duration { get; set; }
        public int StandardTicketsCount { get; set; }
        public int VipTicketsCount { get; set; }
        public int StudentTicketsCount { get; set; }
    }
}
