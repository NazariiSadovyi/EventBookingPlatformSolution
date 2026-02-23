using EBP.Application.DTOs;
using System.ComponentModel;

namespace EBP.API.Models
{
    public class BookTicketsDetailRequest
    {
        [DefaultValue(0)]
        public TicketKindDto Kind { get; set; }
        [DefaultValue(10)]
        public int TicketCount { get; set; }
    }
}
