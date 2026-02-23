using EBP.Application.DTOs;

namespace EBP.API.Models
{
    public class CreateEventTicketDetailRequest
    {
        public TicketKindDto Kind { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
