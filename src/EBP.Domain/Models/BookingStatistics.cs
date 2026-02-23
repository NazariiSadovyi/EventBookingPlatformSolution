namespace EBP.Domain.Models
{
    public class BookingStatistics
    {
        public Guid EventId { get; }
        public string EventName { get; }
        public int TotalBookings { get; }
        public int BookedCount { get; }
        public int SubmittedCount { get; }
        public int CancelledCount { get; }
        public int ReleasedCount { get; }
        public int ExpiredCount { get; }
        public int UsedCount { get; }

        public BookingStatistics(
            Guid eventId,
            string eventName,
            int totalBookings,
            int bookedCount,
            int submittedCount,
            int cancelledCount,
            int releasedCount,
            int expiredCount,
            int usedCount)
        {
            EventId = eventId;
            EventName = eventName;
            TotalBookings = totalBookings;
            BookedCount = bookedCount;
            SubmittedCount = submittedCount;
            CancelledCount = cancelledCount;
            ReleasedCount = releasedCount;
            ExpiredCount = expiredCount;
            UsedCount = usedCount;
        }
    }
}