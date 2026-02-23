using EBP.Domain.Enums;
using EBP.Domain.Exceptions;

namespace EBP.Domain.Entities
{
    public class Booking
    {
        private readonly List<Ticket> _tickets = new();

        public Guid Id { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string UserId { get; private set; }
        public Event Event { get; private set; }
        public IReadOnlyCollection<Ticket> Tickets => _tickets;

        private Booking() { }

        public static Booking CreateNew(Event @event, IEnumerable<Ticket> tickets, string userId, DateTime now)
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Event = @event,
                UserId = userId,
                Status = BookingStatus.Booked,
                CreatedAt = now,
            };

            foreach (var ticket in tickets)
                booking._tickets.Add(ticket);

            return booking;
        }

        public void ExpiryBooking()
        {
            if (Status != BookingStatus.Booked)
                throw new IncorrectBookingStatusException(Id, Status, BookingStatus.Booked);

            Status = BookingStatus.Expired;
            _tickets.Clear();
        }

        public void ReleaseBooking()
        {
            if (Status != BookingStatus.Booked)
                throw new IncorrectBookingStatusException(Id, Status, BookingStatus.Booked);

            Status = BookingStatus.Released;
            _tickets.Clear();
        }

        public void SubmitBooking()
        {
            if (Status != BookingStatus.Booked)
                throw new IncorrectBookingStatusException(Id, Status, BookingStatus.Booked);

            Status = BookingStatus.Submitted;
        }

        public void CancelBooking()
        {
            if (Status != BookingStatus.Submitted)
                throw new IncorrectBookingStatusException(Id, Status, BookingStatus.Booked);

            Status = BookingStatus.Cancelled;
            _tickets.Clear();
        }
    }
}
