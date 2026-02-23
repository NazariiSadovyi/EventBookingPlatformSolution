using EBP.Application.DTOs;
using System.ComponentModel;

namespace EBP.API.Models
{
    public class AddTicketRequest
    {
        [DefaultValue(nameof(TicketKindDto.Regular))]
        public TicketKindDto Kind { get; set; }
        [DefaultValue(10)]
        public decimal Price { get; set; }
    }
}