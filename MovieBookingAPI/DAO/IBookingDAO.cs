using MovieBooking.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public interface IBookingDAO
    {
        Task<int> CreateBookingAsync(int userId, int showtimeId, List<int> seatIds);
        Task ConfirmPaymentAsync(int bookingId, int userId, string paymentMethod);
        Task<List<BookingHistoryRawDTO>> GetBookingHistoryAsync(int userId);

    }
}