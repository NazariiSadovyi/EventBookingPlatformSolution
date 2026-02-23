namespace EBP.Application.DTOs
{
    public record class BookingStatisticsDto(
        Guid EventId,
        string EventName,
        int TotalBookings,
        int BookedCount,
        int SubmittedCount,
        int CancelledCount,
        int ReleasedCount,
        int ExpiredCount,
        int UsedCount);
}