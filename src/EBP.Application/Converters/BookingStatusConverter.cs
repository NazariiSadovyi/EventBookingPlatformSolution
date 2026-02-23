using EBP.Application.DTOs;
using EBP.Domain.Enums;

namespace EBP.Application.Converters
{
    public static class BookingStatusConverter
    {
        public static BookingStatusDto ToDto(this BookingStatus bookingStatus)
        {
            return bookingStatus switch
            {
                BookingStatus.Booked => BookingStatusDto.Booked,
                BookingStatus.Released => BookingStatusDto.Released,
                BookingStatus.Submitted => BookingStatusDto.Submitted,
                BookingStatus.Expired => BookingStatusDto.Expired,
                BookingStatus.Cancelled => BookingStatusDto.Cancelled,
                BookingStatus.Used => BookingStatusDto.Used,
                _ => throw new ArgumentOutOfRangeException(nameof(bookingStatus), $"Not expected booking status value: {bookingStatus}"),
            };
        }
    }
}
