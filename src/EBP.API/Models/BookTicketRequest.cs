using EBP.Domain.Enums;

namespace EBP.API.Models
{
    public class BookTicketRequest
    {
        public BookTicketsDetailRequest[] BookTickets { get; set; } = [];
    }
}
