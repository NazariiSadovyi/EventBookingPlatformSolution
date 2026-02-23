namespace EBP.API.Models
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public TicketTypeContract Type { get; set; }
    }
}
