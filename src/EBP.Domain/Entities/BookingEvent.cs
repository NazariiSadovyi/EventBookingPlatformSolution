using EBP.Domain.Enums;
using EBP.Domain.Exceptions;

namespace EBP.Domain.Entities
{
    public class BookingEvent
    {
        private readonly List<BookingTicket> _tickets = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Desciption { get; private set; }
        public DateTime StartAt { get; private set; }
        public TimeSpan Duration { get; private set; }
        public IReadOnlyCollection<BookingTicket> Tickets => _tickets.AsReadOnly();

        private BookingEvent() { }

        public static BookingEvent CreateNew(
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
                throw new BookingEventIncorrectStartDateException(startAt, now);

            var bookingEvent = new BookingEvent
            {
                Id = Guid.NewGuid(),
                Name = name,
                Desciption = description,
                StartAt = startAt,
                Duration = duration,
            };

            bookingEvent.AddTickets(TicketType.Standard, standartTicketsCount);
            bookingEvent.AddTickets(TicketType.VIP, vipTicketsCount);
            bookingEvent.AddTickets(TicketType.Student, studentTicketsCount);

            return bookingEvent;
        }

        public void AddTickets(TicketType ticketType, int count)
        {
            _tickets.AddRange(Enumerable.Range(0, count).Select(_ => BookingTicket.CreateNew(this, ticketType)));
        }
    }
}
