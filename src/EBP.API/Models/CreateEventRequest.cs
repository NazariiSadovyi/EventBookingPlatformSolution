using System.ComponentModel;

namespace EBP.API.Models
{
    public class CreateEventRequest
    {
        [DefaultValue("Sample Event")]
        public string? Name { get; set; }
        [DefaultValue("This is a sample event description.")]
        public string? Description { get; set; }
        [DefaultValue("2027-01-01T20:00:00Z")]
        public DateTime StartAt { get; set; }
        [DefaultValue("02:00:00")]
        public TimeSpan Duration { get; set; }
        public CreateEventTicketDetailRequest[] TicketDetails { get; set; } = [];
    }
}
