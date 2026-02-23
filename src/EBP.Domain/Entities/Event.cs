using EBP.Domain.Enums;
using EBP.Domain.Exceptions;

namespace EBP.Domain.Entities
{
    public class Event
    {
        private readonly List<Ticket> _tickets = new();
        private readonly List<Booking> _bookings = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Desciption { get; private set; }
        public DateTime StartAt { get; private set; }
        public TimeSpan Duration { get; private set; }
        public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();
        public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

        private Event() { }

        public static Event CreateNew(
            string name,
            string? description,
            DateTime startAt,
            TimeSpan duration,
            (TicketKind kind, decimal price, int count)[] ticketDetails,
            DateTime now)
        {
            if (startAt < now)
                throw new EventIncorrectStartDateException(startAt, now);

            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Name = name,
                Desciption = description,
                StartAt = startAt,
                Duration = duration,
            };

            foreach (var ticketDetail in ticketDetails)
                @event.AddTickets(ticketDetail.kind, ticketDetail.price, ticketDetail.count);

            return @event;
        }

        public void AddTickets(TicketKind ticketKind, decimal price, int count)
        {
            _tickets.AddRange(Enumerable.Range(0, count).Select(_ => Ticket.CreateNew(this, ticketKind, price)));
        }

        public Ticket AddTicket(TicketKind ticketKind, decimal price)
        {
            var ticket = Ticket.CreateNew(this, ticketKind, price);
            _tickets.Add(ticket);
            return ticket;
        }

        public void RemoveTicket(Guid ticketId)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket is null)
                throw new TicketNotFoundException(ticketId);

            if (!ticket.IsAvailable)
                throw new TicketIsInUseException(ticketId);

            _tickets.Remove(ticket);
        }
    }
}
