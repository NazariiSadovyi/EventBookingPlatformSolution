using EBP.Application.DTOs;

namespace EBP.API.Models
{
    public class AddTicketRequest
    {
        public TicketKindDto Kind { get; set; }
        public decimal Price { get; set; }
    }
}