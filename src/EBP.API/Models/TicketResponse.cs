namespace EBP.API.Models
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public TicketKindContract Type { get; set; }
    }
}
