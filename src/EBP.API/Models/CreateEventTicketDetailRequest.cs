namespace EBP.API.Models
{
    public class CreateEventTicketDetailRequest
    {
        public TicketKindContract Kind { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
