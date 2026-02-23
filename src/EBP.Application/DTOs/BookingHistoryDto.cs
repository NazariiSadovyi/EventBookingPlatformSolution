namespace EBP.Application.DTOs
{
    public record class BookingHistoryDto(
        Guid Id,
        string EventName,
        BookingStatusDto Status)
    {
    }
}
