using System.ComponentModel;

namespace EBP.API.Models
{
    public class CreateBookingEventRequest
    {
        [DefaultValue("Sample Event")]
        public string? Title { get; set; }
        [DefaultValue("This is a sample event description.")]
        public string? Description { get; set; }
        [DefaultValue("2027-01-01T20:00:00Z")]
        public DateTime StartAt { get; set; }
        [DefaultValue("02:00:00")]
        public TimeSpan Duration { get; set; }
        [DefaultValue(10)]
        public int StandardTicketsCount { get; set; }
        [DefaultValue(2)]
        public int VipTicketsCount { get; set; }
        [DefaultValue(5)]
        public int StudentTicketsCount { get; set; }
    }
}
