namespace EBP.Domain.Entities
{
    public class BookingRefund
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public string UserId { get; private set; } = null!;
        public Booking Booking { get; private set; } = null!;
        public bool IsRefunded { get; private set; } = false;

        private BookingRefund() { }

        public static BookingRefund CreateNew(Booking booking, string userId)
        {
            return new BookingRefund
            {
                Id = Guid.NewGuid(),
                Amount = booking.Tickets.Sum(_ => _.Type.Price),
                UserId = userId,
                Booking = booking
            };
        }

        public void ProcessRefund()
        {
            IsRefunded = true;
        }
    }
}
