namespace EBP.Application.DTOs
{
    public class EventTicketDetailsDto
    {
        public TicketKindDto Kind { get; set; }
        public decimal Price { get; set; }
        public int BookedCount { get; set; }
        public int AvailableCount { get; set; }
    }
}