using EBP.Application.DTOs;
using EBP.Domain.Entities;

namespace EBP.Application.Converters
{
    public static class BookingHistoryConverter
    {
        public static BookingHistoryDto ToDto(this Booking booking)
        {
            return new BookingHistoryDto(
                booking.Id,
                booking.Event.Name,
                booking.Status.ToDto());
        }
    }
}
