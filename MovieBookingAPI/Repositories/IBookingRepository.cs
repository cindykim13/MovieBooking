using MovieBookingAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<int> CreateBookingAsync(int userId, int showtimeId, List<int> seatIds);
        Task ConfirmPaymentAsync(int bookingId, int userId, string paymentMethod);
        Task<List<BookingHistoryRawDto>> GetBookingHistoryAsync(int userId);

    }
}