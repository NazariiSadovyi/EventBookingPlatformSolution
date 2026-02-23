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
            int standartTicketsCount,
            int vipTicketsCount,
            int studentTicketsCount,
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

            @event.AddTickets(TicketType.Standard, standartTicketsCount);
            @event.AddTickets(TicketType.VIP, vipTicketsCount);
            @event.AddTickets(TicketType.Student, studentTicketsCount);

            return @event;
        }

        public void AddTickets(TicketType ticketType, int count)
        {
            _tickets.AddRange(Enumerable.Range(0, count).Select(_ => Ticket.CreateNew(this, ticketType)));
        }
    }
}
