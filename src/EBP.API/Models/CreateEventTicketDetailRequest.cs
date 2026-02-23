using EBP.Application.DTOs;
using System.ComponentModel;

namespace EBP.API.Models
{
    public class CreateEventTicketDetailRequest
    {
        [DefaultValue(nameof(TicketKindDto.Regular))]
        public TicketKindDto Kind { get; set; }
        [DefaultValue(10)]
        public decimal Price { get; set; }
        [DefaultValue(10)]
        public int Count { get; set; }
    }
}
