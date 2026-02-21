using EBP.Domain.Exceptions;

namespace EBP.Domain.Entities
{
    public class BookingEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Desciption { get; private set; }
        public DateTime StartAt { get; private set; }
        public TimeSpan Duration { get; private set; }

        private BookingEvent() { }

        public static BookingEvent CreateNew(string name, string? description, DateTime startAt, TimeSpan duration, DateTime now)
        {
            if (startAt < now)
                throw new BookingEventIncorrectStartDateException(startAt, now);

            return new BookingEvent
            {
                Id = Guid.NewGuid(),
                Name = name,
                Desciption = description,
                StartAt = startAt,
                Duration = duration
            };
        }
    }
}
