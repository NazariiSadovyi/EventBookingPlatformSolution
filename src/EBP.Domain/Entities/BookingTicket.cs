using EBP.Domain.Enums;
using EBP.Domain.Exceptions;

namespace EBP.Domain.Entities
{
    public class BookingTicket
    {
        public Guid Id { get; private set; }
        public TicketStatus Status { get; private set; }
        public TicketType Type { get; private set; }
        public DateTime? BookedAt { get; private set; }

        public BookingEvent BookingEvent { get; private set; }

        private BookingTicket() { }

        public static BookingTicket CreateNew(BookingEvent bookingEvent, TicketType ticketType)
        {
            return new BookingTicket
            {
                Id = Guid.NewGuid(),
                Status = TicketStatus.Available,
                Type = ticketType,
                BookingEvent = bookingEvent
            };
        }

        public void BookForPurchasing(DateTime currentDateTime)
        {
            if (Status != TicketStatus.Available)
                throw new BookingTicketIsNotAvailableForBookingException(Id);

            Status = TicketStatus.Booked;
            BookedAt = currentDateTime;
        }

        public void ReleaseBooking()
        {
            Status = TicketStatus.Available;
            BookedAt = null;
        }
    }
}
