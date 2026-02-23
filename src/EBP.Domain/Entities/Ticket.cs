using EBP.Domain.Enums;
using EBP.Domain.ValueObjects;

namespace EBP.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; private set; }
        public DateTime? BookedAt { get; private set; }
        public Event Event { get; private set; }
        public TicketType Type { get; private set; }
        public Booking? Booking { get; private set; }
        public bool IsAvailable => Booking is null;

        private Ticket() { }

        public static Ticket CreateNew(Event @event, TicketKind ticketKind, decimal price)
        {
            return new Ticket
            {
                Id = Guid.NewGuid(),
                Type = TicketType.Create(ticketKind, price),
            };
        }
    }
}
